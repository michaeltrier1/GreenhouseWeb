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
            JObject interpretedMessage = new JObject();

            string procedure = (string)jsonMessage.GetValue("procedure");

            string greenHouseID;
            string port;

            switch (procedure)
            {
                case "Startup":
                    greenHouseID = jsonMessage.GetValue("greenhouseID").ToString();
                    port = jsonMessage.GetValue("port").ToString();

                    interpretedMessage.Add("id", greenHouseID);
                    interpretedMessage.Add("port", port);
                    interpretedMessage.Add("procedure", procedure);
                    break;
                case "petWatchdog":
                    greenHouseID = (string)jsonMessage.GetValue("greenhouseID");

                    interpretedMessage.Add("id", greenHouseID);
                    interpretedMessage.Add("procedure", procedure);
                    break;
                case "live data":
                    greenHouseID = (string)jsonMessage.GetValue("greenhouseID");

                    Nullable<double> internalTemperature = (Nullable<double>)jsonMessage.GetValue("internal temperature");
                    Nullable<double> externalTemperature = (Nullable<double>)jsonMessage.GetValue("external temperature");
                    Nullable<double> humidity = (Nullable<double>)jsonMessage.GetValue("humidity");
                    Nullable<double> waterLevel = (Nullable<double>)jsonMessage.GetValue("water level");

                    interpretedMessage.Add("id", greenHouseID);
                    interpretedMessage.Add("internal temperature", internalTemperature);
                    interpretedMessage.Add("external temperature", externalTemperature);
                    interpretedMessage.Add("humidity", humidity);
                    interpretedMessage.Add("water level", waterLevel);
                    interpretedMessage.Add("procedure", procedure);
                    break;
                case "IPAddress":
                    port = jsonMessage.GetValue("port").ToString();
                    greenHouseID = jsonMessage.GetValue("greenhouseID").ToString();

                    interpretedMessage.Add("id", greenHouseID);
                    interpretedMessage.Add("port", port);
                    interpretedMessage.Add("procedure", procedure);
                    break;
                case "Datalog":
                    greenHouseID = jsonMessage.GetValue("greenhouseID").ToString();
                    //long timeOfReading = (long)jsonMessage.GetValue("time of Reading");

                    string timeOfReadingString = (string)jsonMessage.GetValue("time of Reading");
                    DateTime timeOfReading = DateTime.Parse(timeOfReadingString, System.Globalization.CultureInfo.InvariantCulture);
                    Nullable<double> internalTemperatureDatalog = (Nullable<double>)jsonMessage.GetValue("internal temperature");
                    Nullable<double> externalTemperatureDatalog = (Nullable<double>)jsonMessage.GetValue("external temperature");
                    Nullable<double> humidityDatalog = (Nullable<double>)jsonMessage.GetValue("humidity");
                    Nullable<double> waterLevelDatalog = (Nullable<double>)jsonMessage.GetValue("waterlevel");

                    interpretedMessage.Add("id", greenHouseID);
                    interpretedMessage.Add("internal temperature", internalTemperatureDatalog);
                    interpretedMessage.Add("external temperature", externalTemperatureDatalog);
                    interpretedMessage.Add("humidity", humidityDatalog);
                    interpretedMessage.Add("water level", waterLevelDatalog);
                    interpretedMessage.Add("procedure", procedure);
                    interpretedMessage.Add("time of reading", timeOfReading);
                    break;
                default:
                    break;
            }


            return interpretedMessage;
        }

    }
}