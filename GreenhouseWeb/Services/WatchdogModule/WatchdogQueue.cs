using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace GreenhouseWeb.Services.WatchdogModule
{
    public class WatchdogQueue
    {
        private static int pettingLock = 0;

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
                if (0 == Interlocked.Exchange(ref pettingLock, 1))
                {
                    this.pettingQueue.Add(greenhouseID);
                    Interlocked.Exchange(ref pettingLock, 0);
                    accessed = true;
                }
                else
                {
                    Thread.Sleep(200);
                }
            }
        }

        public HashSet<string> getPettings()
        {
            bool accessed = false;
            HashSet<string> temp = null;

            while (!accessed)
            {
                if (0 == Interlocked.Exchange(ref pettingLock, 1))
                {
                    temp = new HashSet<string>(this.pettingQueue);
                    this.pettingQueue = new HashSet<string>();
                    Interlocked.Exchange(ref pettingLock, 0);
                    return temp;
                }
                else
                {
                    Thread.Sleep(30);
                }
            }

            return temp;
        }

        internal void putPetting(HashSet<string> toBePetted)
        {
            bool accessed = false;
            while (!accessed)
            {
                if (0 == Interlocked.Exchange(ref pettingLock, 1))
                {
                    this.pettingQueue.UnionWith(toBePetted);
                    Interlocked.Exchange(ref pettingLock, 0);
                    accessed = true;
                }
                else
                {
                    Thread.Sleep(30);
                }
            }
        }
    }
}