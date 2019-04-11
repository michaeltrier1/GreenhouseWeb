using GreenhouseWeb.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenhouseWeb.Services
{
    public class Measurements : IMeasurement
    {
        public double internalTemperature { get; set; }
        public double externalTemperature { get; set; }
        public double humidity { get; set; }
        public double waterlevel { get; set; }
        
        public Measurements(double internalTemperature, double externalTemperature, double humidity, double waterlevel)
        {
            this.internalTemperature = internalTemperature;
            this.externalTemperature = externalTemperature;
            this.humidity = humidity;
            this.waterlevel = waterlevel;
        }
    }
}