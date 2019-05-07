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
        private Thread watchdogThread;
        private WatchdogQueue queue;

        public WatchdogFacade(IServicesFacadeForServices servicesFacade)
        {
            this.servicesFacade = servicesFacade;
            this.queue = new WatchdogQueue();
        
            watchdog = new Watchdog(this, queue);

            watchdogThread = new Thread(new ThreadStart(watchdog.StartWatchdog));
            watchdogThread.Start();
        }

        public void PetWatchdog(string greenhouseID) {

            this.queue.putPetting(greenhouseID);
        }

        public void RetryConnection(string greenhouseID)
        {
            servicesFacade.RetryConnection(greenhouseID);
        }

        internal void clear()
        {
            watchdogThread.Interrupt();
        }

    }
}