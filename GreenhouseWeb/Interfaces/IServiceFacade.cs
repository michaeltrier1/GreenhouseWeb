using GreenhouseWeb.Services.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenhouseWeb.Interfaces
{
    public interface IServiceFacade
    {
        IMeasurement getCurrentLiveData(String greenhouseID);
        void stopLiveData(string greenhouseID);
        void applySchedule(string greenhouseID, JObject schedule);
        void clear();
    }
}
