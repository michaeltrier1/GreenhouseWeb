using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace GreenhouseWeb.Models
{
    public class GreenhouseDBContext : DbContext
    {
        public DbSet<Greenhouse> Greenhouses { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
    }
}