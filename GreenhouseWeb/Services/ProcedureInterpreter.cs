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

                    break;
                case "live data":
                    break;
                default:
                    break;
            }





        }

    }
}