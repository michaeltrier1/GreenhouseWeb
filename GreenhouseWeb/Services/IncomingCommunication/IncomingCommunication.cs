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

namespace GreenhouseWeb.Services.Incoming
{
    public class IncomingCommunicator
    {

        private IPAddress ipAddress;
        private TcpListener listener;
        private IServicesFacadeForServices servicesFacade;

        public IncomingCommunicator(IServicesFacadeForServices servicesFacade)
        {
            this.servicesFacade = servicesFacade;
            this.ipAddress = IPAddress.Parse("127.0.0.1");
            this.listener = new TcpListener(ipAddress, 8090);
        }       

        public void listenForConnections()
        {
            while (true)
            {
                try
                {
                    listener.Start();

                    TcpClient client = listener.AcceptTcpClient();

                    SocketHandler socketHandler = new SocketHandler(client, this);

                    Thread thread = new Thread(new ThreadStart(socketHandler.handleSocket));
                    thread.Start();


                }
                catch (IOException e)
                {
                    Console.Write(e.StackTrace);
                }
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


    }
}