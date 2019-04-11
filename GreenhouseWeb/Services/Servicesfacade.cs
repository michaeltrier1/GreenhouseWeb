using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GreenhouseWeb.Services.WatchdogModule;
using GreenhouseWeb.Services.Interfaces;
using GreenhouseWeb.Services.Communication;

namespace GreenhouseWeb.Services
{
    public class ServicesFacade : IServicesFacadeForServices
    {
        private IncomingCommunicator incommingCommunication;
        private LiveData liveData;
        private WatchdogFacade wactchdogFacade;
        private CommunicationFacade communicationFacade;

        public ServicesFacade()
        {
            this.incommingCommunication = new IncomingCommunicator(this);
            this.liveData = new LiveData();
            this.wactchdogFacade = new WatchdogFacade(this);
            this.communicationFacade = new CommunicationFacade();

        }

        public IMeasurement getLCurrentLiveData(String greenhouseID)
        {
            return this.liveData.getMeasurements(greenhouseID);
        }

        public void PetWatchdog(string greenhouseID)
        {
            wactchdogFacade.PetWatchdog(greenhouseID);
        }

        public void RetryConnection(string greenhouseID)
        {
            communicationFacade.RetryConnection(greenhouseID);
        }

        public void SetMeasurement(string greenhouseID, IMeasurement measurement)
        {
            this.liveData.setMeasurements(greenhouseID, measurement);
        }
    }
}