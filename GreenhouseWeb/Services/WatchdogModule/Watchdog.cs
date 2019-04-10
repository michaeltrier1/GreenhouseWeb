using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GreenhouseWeb.Services.Interfaces;

namespace GreenhouseWeb.Services.WatchdogModule
{
    public class Watchdog
    {

       
        private static Dictionary<string, DateTime> greenhouses;
        private WatchdogFacade watchdogFacade;

        public Watchdog(WatchdogFacade facade)
        {
            watchdogFacade = facade;
        }

        public static void PetWatchdog(string greenhouseID)
        {
            if (greenhouses.ContainsKey(greenhouseID))
            {
                greenhouses[greenhouseID] = DateTime.Now;
            }
            else
            {
                greenhouses.Add(greenhouseID, DateTime.Now);
            }
        }

        public void StartWatchdog()
        {
            Boolean stopped = false;
            while (!stopped)
            {

                foreach (KeyValuePair<string, DateTime> entry in greenhouses)
                {
                    DateTime now = DateTime.Now;
                    TimeSpan difference = now.Subtract(entry.Value);
                    if (difference.Minutes > 2)
                    {
                        RetryConnection(entry.Key);
                    }

                }

            }

        }

        private void RetryConnection(string greenhouseID)
        {
            watchdogFacade.RetryConnection(greenhouseID);
        }

    }

}