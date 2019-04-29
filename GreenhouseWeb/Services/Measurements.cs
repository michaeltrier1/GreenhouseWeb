using GreenhouseWeb.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenhouseWeb.Services
{
    public class Measurements : IMeasurement
    {
        public double InternalTemperature { get; set; }
        public double ExternalTemperature { get; set; }
        public double Humidity { get; set; }
        public double Waterlevel { get; set; }
        
        public Measurements(double internalTemperature, double externalTemperature, double humidity, double waterlevel)
        {
            this.InternalTemperature = internalTemperature;
            this.ExternalTemperature = externalTemperature;
            this.Humidity = humidity;
            this.Waterlevel = waterlevel;
        }
    }
}