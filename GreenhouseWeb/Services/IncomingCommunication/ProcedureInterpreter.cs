using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenhouseWeb.Services.Incoming
{
    public class ProcedureInterpreter
    {

        public JObject interpret(String message)
        {
            JObject jsonMessage = JObject.Parse(message);
            JObject interpretedMessage = new JObject("{ }");

            string procedure = (string)jsonMessage.GetValue("procedure");

            string greenHouseID;
            string port;

            switch (procedure)
            {
                case "Startup":
                    greenHouseID = (string)jsonMessage.GetValue("id");
                    port = (string)jsonMessage.GetValue("port");

                    interpretedMessage.Add("id", greenHouseID);
                    interpretedMessage.Add("port", port);
                    interpretedMessage.Add("procedure", procedure);
                    break;
                case "petWatchdog":
                    greenHouseID = (string)jsonMessage.GetValue("id");

                    interpretedMessage.Add("id", greenHouseID);
                    interpretedMessage.Add("procedure", procedure);
                    break;
                case "live data":
                    greenHouseID = (string)jsonMessage.GetValue("id");

                    double internalTemperature = Double.Parse((string)jsonMessage.GetValue("internal temperature"));
                    double externalTemperature = Double.Parse((string)jsonMessage.GetValue("external temperature"));
                    double humidity = Double.Parse((string)jsonMessage.GetValue("humidity"));
                    double waterLevel = Double.Parse((string)jsonMessage.GetValue("water level"));

                    interpretedMessage.Add("id", greenHouseID);
                    interpretedMessage.Add("internal temperature", internalTemperature);
                    interpretedMessage.Add("external temperature", externalTemperature);
                    interpretedMessage.Add("humidity", humidity);
                    interpretedMessage.Add("water level", waterLevel);
                    break;
                case "IPAddress":
                    port = (string)jsonMessage.GetValue("port");
                    greenHouseID = (string)jsonMessage.GetValue("id");

                    interpretedMessage.Add("id", greenHouseID);
                    interpretedMessage.Add("port", port);
                    interpretedMessage.Add("procedure", procedure);
                    break;
                default:
                    break;
            }


            return interpretedMessage;
        }

    }
}