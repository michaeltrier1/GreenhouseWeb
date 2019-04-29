using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GreenhouseWeb.Models
{
    public class GreenhouseDBContext : DbContext
    {
        public DbSet<Greenhouse> Greenhouses { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
    }
}