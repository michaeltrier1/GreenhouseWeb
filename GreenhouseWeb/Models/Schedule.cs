using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenhouseWeb.Models
{
    public class Schedule
    {
        public int ID { get; set; }
        public string ScheduleID { get; set; }
        public int Blocknumber { get; set; }
        public double InternalTemperature  { get; set; }
        public double ExternalTemperature  { get; set; }
        public double Humidity  { get; set; }
        public double WaterLevel  { get; set; }
        public double RedLight  { get; set; }
        public double BlueLight  { get; set; }

}
        
}
