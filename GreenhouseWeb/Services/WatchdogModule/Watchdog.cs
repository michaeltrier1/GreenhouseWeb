﻿using System;
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
        private WatchdogQueue queue;

        public Watchdog(WatchdogFacade facade, WatchdogQueue queue)
        {
            watchdogFacade = facade;
            this.queue = queue;
        }

        private void PetWatchdog(string greenhouseID)
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
                HashSet<string> tmpqueu = queue.getPettings();
                foreach (string greenhouseID in tmpqueu)
                {
                    this.PetWatchdog(greenhouseID);
                }

                HashSet<string> toBePetted = new HashSet<string>();
                foreach (KeyValuePair<string, DateTime> entry in greenhouses)
                {
                    DateTime now = DateTime.Now;
                    TimeSpan difference = now.Subtract(entry.Value);
                    //if (difference.Minutes > 2) //for production
                    if (difference.Seconds > 10) //TODO for testing
                    {
                        RetryConnection(entry.Key);
                        toBePetted.Add(entry.Key);
                    }
                }

                queue.putPetting(toBePetted);

                //try  { Thread.Sleep(15000); } //for production
                try { Thread.Sleep(5000); } //TODO for testing
                catch (ThreadInterruptedException e) { stopped = true; }
            }
        }

        private void RetryConnection(string greenhouseID)
        {
            watchdogFacade.RetryConnection(greenhouseID);
        }

    }

}