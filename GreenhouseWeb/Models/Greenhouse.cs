using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenhouseWeb.Models
{
    public class Greenhouse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get;}
        public string GreenhouseID { get; set; }
        public string Password { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }


    }

 
}