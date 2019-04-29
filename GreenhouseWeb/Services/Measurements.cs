using GreenhouseWeb.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenhouseWeb.Services
{
    public class Measurements : IMeasurement
    {
        public Nullable<double> InternalTemperature { get; set; }
        public Nullable<double> ExternalTemperature { get; set; }
        public Nullable<double> Humidity { get; set; }
        public Nullable<double> Waterlevel { get; set; }
        
        public Measurements(Nullable<double> internalTemperature, Nullable<double> externalTemperature, Nullable<double> humidity, Nullable<double> waterlevel)
        {
            this.InternalTemperature = internalTemperature;
            this.ExternalTemperature = externalTemperature;
            this.Humidity = humidity;
            this.Waterlevel = waterlevel;
        }
    }
}