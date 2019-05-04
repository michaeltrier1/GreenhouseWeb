using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Web;
using GreenhouseWeb.Services;
using GreenhouseWeb.Services.Interfaces;
using Newtonsoft.Json.Linq;
using GreenhouseWeb.Models;
using System.Data.Entity;

namespace GreenhouseWeb.Services.Incoming
{
    public class IncomingCommunicator
    {

        private IPAddress ipAddress;
        private TcpListener listener;
        private IServicesFacadeForServices servicesFacade;
        private Dictionary<string, SocketHandler> activeHandlers;

        public IncomingCommunicator(IServicesFacadeForServices servicesFacade)
        {
            this.servicesFacade = servicesFacade;
            this.ipAddress = IPAddress.Parse("127.0.0.1"); //TODO set right ip
            this.listener = new TcpListener(ipAddress, 8090);
            this.activeHandlers = new Dictionary<string, SocketHandler>();
        }

        public void listenForConnections()
        {
            bool stopped = false;
            while (!stopped)
            {
                try
                {
                    listener.Start();

                    TcpClient client = listener.AcceptTcpClient();
                    SocketHandler socketHandler = new SocketHandler(client, this);

                    Thread thread = new Thread(new ThreadStart(socketHandler.handleSocket));
                    thread.Start();
                }
                catch (IOException e) { Console.Write(e.StackTrace); }
                catch (SocketException e) { stopped = true; }
            }
        }

        internal void petWatchdog(string greenhouseID)
        {
            this.servicesFacade.PetWatchdog(greenhouseID);
        }

        internal void setMeasurements(string greenhouseID, Measurements measurements)
        {
            this.servicesFacade.SetMeasurement(greenhouseID, measurements);
        }

        internal void registerSocketHandler(string greenhouseID, SocketHandler handler)
        {
            this.activeHandlers.Add(greenhouseID, handler);
        }

        internal void stopLiveData(string greenhouseID)
        {
            if (greenhouseID == "") { }
            else
            {
                this.activeHandlers[greenhouseID].stop();
            }
        }

        internal void unregisterSocketHandler(string greenhouseID)
        {
            this.activeHandlers.Remove(greenhouseID);
        }

        internal void unregisterSocketHandler(object registerID)
        {
            throw new NotImplementedException();
        }

        internal void setIPAddress(string id, string ip, string port)
        {
            GreenhouseDBContext db = new GreenhouseDBContext();
            Greenhouse greenhouse = new Greenhouse();

            foreach (Greenhouse item in db.Greenhouses)
            {
                if (item.GreenhouseID == id)
                {
                    db.Greenhouses.Remove(item);
                }
            }
            greenhouse.GreenhouseID = id;
            greenhouse.IP = ip;
            greenhouse.Port = int.Parse(port);
            greenhouse.Password = "nothing";

           

            
          
                db.Greenhouses.Add(greenhouse);
            

            db.SaveChanges();


        }

        internal JObject fetchSchedule(string greenHouseID)
        {
            return new JObject(); 
            // TODO: 
        }

        public void datalog(string greenHouseID, long timeofreading, Nullable<double> internalTemperature, Nullable<double> externalTemperature, Nullable<double> humidity, Nullable<double> waterlevel)
        {
            GreenhouseDBContext db = new GreenhouseDBContext();
            Datalog datalog = new Datalog();

            datalog.Greenhouse_ID = greenHouseID;
            datalog.TimeOfReading = new DateTime(timeofreading);
            datalog.InternalTemperature = (float)internalTemperature;
            datalog.ExternalTemperature = (float)externalTemperature;
            datalog.Humidity = (float)humidity;
            datalog.Waterlevel = (float)waterlevel;

            db.Datalogs.Add(datalog);
            db.SaveChanges();


        }

        internal void clear()
        {
            listener.Stop();
            foreach (SocketHandler handler in activeHandlers.Values)
            {
                handler.stop();
            }
        }

    }
}