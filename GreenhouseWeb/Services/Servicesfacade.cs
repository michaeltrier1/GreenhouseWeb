using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GreenhouseWeb.Services.WatchdogModule;

namespace GreenhouseWeb.Services
{
    public class ServicesFacade
    {
        private IncomingCommunication incommingCommunication;
        private LiveData liveData;
        private WatchdogFacade wactchdogFacade;

        public ServicesFacade()
        {
            this.incommingCommunication = new IncomingCommunication(this);
            this.liveData = new LiveData();
            this.wactchdogFacade = new WatchdogFacade(this);


        }

        public Measurements getLCurrentLiveData(String greenhouseID)
        {
            return this.liveData.getMeasurements(greenhouseID);
        }



    }
}