using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenhouseWeb.Services
{
    public class LiveData
    {
        private Dictionary<String , Measurements> liveData;

        public LiveData()
        {
            this.liveData = new Dictionary<string, Measurements>();


            this.liveData.Add("testgreenhouse", new Measurements(5,5,5,5));
        }

        public Measurements getMeasurements(String greenhouseID)
        {
            return this.liveData[greenhouseID];
        }


    }
}