using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace GreenhouseWeb.Services.Incoming
{
    public class SocketHandler
    {
        private TcpClient client;
        private IncomingCommunicator incomingCommunicator;
        private ProcedureInterpreter interpreter;
        private bool stopped;
        private bool registered;
        private string registerID;

        public SocketHandler(TcpClient client, IncomingCommunicator incomingCommunicator)
        {
            this.interpreter = new ProcedureInterpreter();
            this.client = client;
            this.incomingCommunicator = incomingCommunicator;
            this.stopped = false;
            this.registered = false;
        }

        public void handleSocket()
        {
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);

            string ip = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();

            bool readMore = true;

            while (readMore && !stopped)
            {
                string message = reader.ReadLine();
                JObject interpretedMessage = this.interpreter.interpret(message);
                JObject response;

                switch ((string)interpretedMessage.GetValue("procedure"))
                {
                    case "petWatchdog":
                        response = this.pet(interpretedMessage);
                        break;
                    case "Startup":
                        response = this.startup(interpretedMessage, ip);
                        break;
                    case "live data":
                        response = this.live(interpretedMessage);
                        break;
                    case "IPAddress":
                        response = this.IP(interpretedMessage, ip);
                        break;
                    default:
                        response = new JObject("{}");
                        break;
                }

                writer.WriteLine(response.ToString());
                writer.Flush();
            }

            if (registered)
            {
                this.incomingCommunicator.unregisterSocketHandler(registerID);
            }
        }

        public void stop()
        {
            this.stopped = true;
        }

        private JObject pet(JObject interpretedMessage)
        {
            string greenHouseID = (string)interpretedMessage.GetValue("id");
            incomingCommunicator.petWatchdog(greenHouseID);

            return new JObject("{ }");
        }

        private JObject IP(JObject interpretedMessage, string ip)
        {
            string greenHouseID = (string)interpretedMessage.GetValue("id");
            string port = (string)interpretedMessage.GetValue("port");
            incomingCommunicator.setIPAddress(greenHouseID, ip, port);

            return new JObject("{ }");
        }

        private JObject live(JObject interpretedMessage)
        {
            string greenHouseID = (string)interpretedMessage.GetValue("id");

            double internalTemperature = (double)interpretedMessage.GetValue("internal temperature");
            double externalTemperature = (double)interpretedMessage.GetValue("external temperature"));
            double humidity = (double)interpretedMessage.GetValue("humidity");
            double waterLevel = (double)interpretedMessage.GetValue("water level");

            Measurements measurements = new Measurements(internalTemperature, externalTemperature, humidity, waterLevel);

            incomingCommunicator.setMeasurements(greenHouseID, measurements);

            if (!registered)
            {
                this.incomingCommunicator.registerSocketHandler(registerID, this);
                registered = true;
            }

            return new JObject("{ }");
        }

        private JObject startup(JObject interpretedMessage, string ip)
        {
            string greenHouseID = (string)interpretedMessage.GetValue("id");
            string port = (string)interpretedMessage.GetValue("port");

            incomingCommunicator.setIPAddress(greenHouseID, ip, port);

            JObject schedule = incomingCommunicator.fetchSchedule(greenHouseID);
            return schedule;
        }
        
    }
    
}