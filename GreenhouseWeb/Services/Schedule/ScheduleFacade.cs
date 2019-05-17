using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenhouseWeb.Services.Schedule
{
    public class ScheduleFacade : IScheduleFacade
    {
        public JObject repackage(JObject rawSchedule)
        {
           return new ScheduleRepacker().repackage(rawSchedule);
        }
    }
}