using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenhouseWeb.Services.Incoming
{
    public class ProcedureInterpreter
    {

        public string interpret(String message, IncomingCommunicator incomingCommunicator)
        {
            JObject jsonMessage = JObject.Parse(message);

            string procedure = (string)jsonMessage.GetValue("procedure");
            jsonMessage.GetValue("procedure");

            string greenHouseID;
            switch (procedure)
            {
                case "watchdog":
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
                default:
                    break;
            }
            return "";
        }

    }
}