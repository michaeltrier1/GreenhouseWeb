using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GreenhouseWeb.Models
{
    public class GreenhouseSchedule
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string GreenhouseID { get; set; }
        public string ScheduleID { get; set; }
        public DateTime StartDate { get; set; }
    }
}