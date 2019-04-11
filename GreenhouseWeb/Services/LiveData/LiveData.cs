using GreenhouseWeb.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenhouseWeb.Services
{
    public class LiveData
    {
        private Dictionary<String , IMeasurement> liveData;

        public LiveData()
        {
            this.liveData = new Dictionary<string, IMeasurement>();
            // TODO remove testvalue
            this.liveData.Add("testgreenhouse", new Measurements(5,5,5,5));
        }

        public IMeasurement getMeasurements(String greenhouseID)
        {
            return this.liveData[greenhouseID];
        }

        public void setMeasurements(string greenhouseID, IMeasurement measurements)
        {
            this.liveData.Add(greenhouseID, measurements);
        }


    }
}