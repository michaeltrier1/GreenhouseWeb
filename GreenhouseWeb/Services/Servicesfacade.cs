using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenhouseWeb.Services
{
    public class Servicesfacade
    {

        private LiveData liveData;

        public Servicesfacade()
        {
            this.liveData = new LiveData();
        }

        public Measurements getCurrentLiveData(String greenhouseID)
        {
            return this.liveData.getMeasurements(greenhouseID);
        }



    }
}