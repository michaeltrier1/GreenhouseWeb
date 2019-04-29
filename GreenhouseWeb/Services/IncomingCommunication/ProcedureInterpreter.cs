using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenhouseWeb.Services.Incoming
{
    public class ProcedureInterpreter
    {

        public string interpret(String message, IncomingCommunicator incomingCommunicator, string ip)
        {
            JObject jsonMessage = JObject.Parse(message);

            string procedure = (string)jsonMessage.GetValue("procedure");

            string greenHouseID;
            switch (procedure)
            {
                case "petWatchdog":
                    greenHouseID = (string)jsonMessage.GetValue("id");
                    incomingCommunicator.petWatchdog(greenHouseID);
                    break;
                case "live data":
                    greenHouseID = (string)jsonMessage.GetValue("id");

                    double internalTemperature = Double.Parse((string)jsonMessage.GetValue("internal temperature"));
                    double externalTemperature = Double.Parse((string)jsonMessage.GetValue("external temperature"));
                    double humidity = Double.Parse((string)jsonMessage.GetValue("humidity"));
                    double waterLevel = Double.Parse((string)jsonMessage.GetValue("water level"));

                    Measurements measurements = new Measurements(internalTemperature, externalTemperature, humidity, waterLevel);
                    incomingCommunicator.setMeasurements(greenHouseID, measurements);
                    return greenHouseID;
                case "IPAddress":
                    String port = (string)jsonMessage.GetValue("port");
                    String id = (string)jsonMessage.GetValue("id");

                    incomingCommunicator.setIPAddress(id, ip, port);
                    break;
                default:
                    break;
            }
            return "";
        }

    }
}