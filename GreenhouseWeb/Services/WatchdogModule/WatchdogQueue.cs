using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace GreenhouseWeb.Services.WatchdogModule
{
    public class WatchdogQueue
    {
        private bool Semaphore { get; set; }
        private HashSet<string> pettingQueue;

        public WatchdogQueue()
        {
            pettingQueue = new HashSet<string>();
        }

        public void putPetting(string greenhouseID)
        {
            bool accessed = false;
            while (!accessed)
            {
                if (!Semaphore)
                {
                    Semaphore = true;
                    this.pettingQueue.Add(greenhouseID);
                    accessed = true;
                    Semaphore = false;
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }

        public HashSet<string> getPettings()
        {
            bool accessed = false;
            HashSet<string> temp = null;

            while (!accessed)
            {
                if (!Semaphore)
                {
                    Semaphore = true;
                    temp = new HashSet<string>(this.pettingQueue);
                    this.pettingQueue = new HashSet<string>();
                    accessed = true;
                    Semaphore = false;
                    return temp;
                }
                else
                {
                    Thread.Sleep(100);
                }
            }

            return temp;
        }

    }
}