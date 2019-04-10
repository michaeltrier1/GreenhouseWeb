using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenhouseWeb.Services
{
    public class ProcedureInterpreter
    {

        public void interpret(String message)
        {
            JObject jsonMessage = JObject.Parse(message);

            string procedure = (string)jsonMessage.GetValue("procedure");
            jsonMessage.GetValue("procedure");

            switch (procedure)
            {
                case "watchdog":
                    string greenHouseID = (string)jsonMessage.GetValue("id");
                    InsertInterfaceModule

                    break;
                case "live data":
                    double internalTemperature = Double.Parse((string)jsonMessage.GetValue("internal temperature"));
                    double externalTemperature = Double.Parse((string)jsonMessage.GetValue("external temperature"));
                    double humidity = Double.Parse((string)jsonMessage.GetValue("humidity"));
                    double waterLevel = Double.Parse((string)jsonMessage.GetValue("water level"));

                    Measurements measurements = new Measurements(internalTemperature, externalTemperature, humidity, waterLevel);

                    insertInferfaceModule
                    break;
                default:
                    break;
            }





        }

    }
}