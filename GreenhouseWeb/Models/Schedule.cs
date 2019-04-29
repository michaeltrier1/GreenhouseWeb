using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GreenhouseWeb.Models
{
    public class Schedule
    {
        [Key]
        public string Schedule_ID { get; set; }
        public int Blocknumber { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float Waterlevel { get; set; }
        public float RedLight { get; set; }
        public float BlueLight { get; set; }

      
    }
}