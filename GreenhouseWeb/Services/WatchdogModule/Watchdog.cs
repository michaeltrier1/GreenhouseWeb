using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using GreenhouseWeb.Services.Interfaces;

namespace GreenhouseWeb.Services.WatchdogModule
{
    public class Watchdog
    {
        private static Dictionary<string, DateTime> greenhouses = new Dictionary<string, DateTime>();
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
                    //if (difference.Minutes > 2) //for production
                    if (difference.Seconds > 3) //TODO for testing
                    {
                        RetryConnection(entry.Key);
                    }

                }

                //try  { Thread.Sleep(15000); } //for production
                try { Thread.Sleep(1000); } //TODO for testing
                catch (ThreadInterruptedException e) { stopped = true; }
            }
        }

        private void RetryConnection(string greenhouseID)
        {
            watchdogFacade.RetryConnection(greenhouseID);
        }

    }

}