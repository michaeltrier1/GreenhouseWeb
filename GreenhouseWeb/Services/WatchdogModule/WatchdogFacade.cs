using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GreenhouseWeb.Services.Interfaces;
using GreenhouseWeb.Services;
using System.Threading;

namespace GreenhouseWeb.Services.WatchdogModule
{
    public class WatchdogFacade
    {
        IServicesFacadeForServices servicesFacade;

        private Watchdog watchdog;
        private Thread thr;
        public WatchdogFacade(IServicesFacadeForServices servicesFacade)
        {
            this.servicesFacade = servicesFacade;
        
            watchdog = new Watchdog(this);

            thr = new Thread(new ThreadStart(watchdog.StartWatchdog));
        }

        public void PetWatchdog(string greenhouseID) => Watchdog.PetWatchdog(greenhouseID);

        public void RetryConnection(string greenhouseID)
        {
            servicesFacade.RetryConnection(greenhouseID);
        }
    }
}