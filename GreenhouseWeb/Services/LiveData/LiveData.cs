﻿using GreenhouseWeb.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace GreenhouseWeb.Services
{
    public class LiveData
    {
        private Dictionary<String , IMeasurement> liveData;
        private IServicesFacadeForServices servicesFacade;

        public LiveData(IServicesFacadeForServices servicesFacade)
        {
            this.servicesFacade = servicesFacade;
            this.liveData = new Dictionary<string, IMeasurement>();
            // TODO remove testvalue
            this.liveData.Add("testgreenhouse", new Measurements(5,5,5,5));
            this.liveData.Add("somethingelse", new Measurements(15, null, 25, 35));
            this.liveData.Add("herpderp", new Measurements(15,15,15,15));
        }

        public IMeasurement getMeasurements(String greenhouseID)
        {
            if (greenhouseID != "")
            {
                if (!this.liveData.ContainsKey(greenhouseID)) {
                    this.liveData.Add(greenhouseID, new Measurements(null, null, null, null));
                    servicesFacade.startLiveDataStream(greenhouseID);
                

            }

                return this.liveData[greenhouseID];
            }
            else
            {
                return new Measurements(0, 0, 0, 0);
            }
        }

        public void setMeasurements(string greenhouseID, IMeasurement measurements)
        {
            this.liveData.Add(greenhouseID, measurements);
        }


    }
}