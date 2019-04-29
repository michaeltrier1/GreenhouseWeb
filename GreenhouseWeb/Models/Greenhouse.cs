using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace GreenhouseWeb.Models
{
    public class Greenhouse
    {

        public int ID { get; set; }
        public string GreenhouseID { get; set; }
        public string Password { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }


    }

 
}