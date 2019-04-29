using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenhouseWeb.Models
{
    public class Datalog
    {
        public string Greenhouse_ID { get; set; }
        public DateTime TimeOfReading { get; set; }
        public float InternalTemperature { get; set; }
        public float ExternalTemperature { get; set; }
        public float Humidity { get; set; }
        public float Waterlevel { get; set; }
    }
}