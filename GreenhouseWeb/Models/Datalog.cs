using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GreenhouseWeb.Models
{
    public class Datalog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set;}

        public string Greenhouse_ID { get; set; }
        public DateTime TimeOfReading { get; set; }
        public float InternalTemperature { get; set; }
        public float ExternalTemperature { get; set; }
        public float Humidity { get; set; }
        public float Waterlevel { get; set; }
    }
}