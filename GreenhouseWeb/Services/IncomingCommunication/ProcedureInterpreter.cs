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
                    greenHouseID = (string)jsonMessage.GetValue("id");

                    interpretedMessage.Add("id", greenHouseID);
                    interpretedMessage.Add("procedure", procedure);
                    break;
                case "live data":
                    greenHouseID = (string)jsonMessage.GetValue("greenhouseID");

                    Nullable<double> internalTemperature = Double.Parse((string)jsonMessage.GetValue("internal temperature"));
                    Nullable<double> externalTemperature = Double.Parse((string)jsonMessage.GetValue("external temperature"));
                    Nullable<double> humidity = Double.Parse((string)jsonMessage.GetValue("humidity"));
                    Nullable<double> waterLevel = Double.Parse((string)jsonMessage.GetValue("water level"));

                    interpretedMessage.Add("id", greenHouseID);
                    interpretedMessage.Add("internal temperature", internalTemperature);
                    interpretedMessage.Add("external temperature", externalTemperature);
                    interpretedMessage.Add("humidity", humidity);
                    interpretedMessage.Add("water level", waterLevel);
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

                    //Nullable<long> timeOfReading = long.Parse((string)jsonMessage.GetValue("time of reading"));
                    //internalTemperature = Double.Parse((string)jsonMessage.GetValue("internal temperature"));
                    //externalTemperature = Double.Parse((string)jsonMessage.GetValue("external temperature"));
                    //humidity = Double.Parse((string)jsonMessage.GetValue("humidity"));
                    //waterLevel = Double.Parse((string)jsonMessage.GetValue("water level"));


                    string timeOfReading = jsonMessage.GetValue("time of reading").ToString();
                    string internalTemperature2 = jsonMessage.GetValue("internal temperature").ToString();
                    string externalTemperature2 = jsonMessage.GetValue("external temperature").ToString();
                    string humidity2 = jsonMessage.GetValue("humidity").ToString();
                    string waterLevel2 = jsonMessage.GetValue("water level").ToString();

                    interpretedMessage.Add("id", greenHouseID);
                    interpretedMessage.Add("internal temperature", internalTemperature2);
                    interpretedMessage.Add("external temperature", externalTemperature2);
                    interpretedMessage.Add("humidity", humidity2);
                    interpretedMessage.Add("water level", waterLevel2);

                    break;
                default:
                    break;
            }


            return interpretedMessage;
        }

    }
}