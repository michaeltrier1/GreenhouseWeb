using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GreenhouseWeb.Services.Interfaces;
using GreenhouseWeb.Services;
using System.Threading;

namespace GreenhouseWeb.Services.WatchdogModule
{
    public class WatchdogFacade : IWatchdogFacade
    {
        public ServicesFacade ServicesFadace { get; }

        private Watchdog watchdog;
        private Thread thr;
        public WatchdogFacade(ServicesFacade servicesFacade)
        {
            this.ServicesFadace = servicesFacade;
        
        watchdog = new Watchdog();

            thr = new Thread(new ThreadStart(watchdog.StartWatchdog));
        }

        public void PetWatchdog(string greenhouseID) => Watchdog.PetWatchdog(greenhouseID);

    }
}