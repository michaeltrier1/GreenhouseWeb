using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace GreenhouseWeb.Models
{
    public class Greenhouse
    {
        [Key]
        public string Greenhouse_ID { get; set; }
        public string Password { get; set; }
        public string ip { get; set; }
        public string portnumber { get; set; }


    }
  
}
