using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenhouseWeb.Services.Interfaces
{
    public interface IServicesFacadeForServices
    {
        void PetWatchdog(string greenhouseID);
        void RetryConnection(string greenhouseID);
        void SetMeasurement(string greenhouseID, IMeasurement measurement);
    }
}
