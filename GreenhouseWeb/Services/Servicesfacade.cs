using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GreenhouseWeb.Services.WatchdogModule;
using GreenhouseWeb.Services.Interfaces;
using GreenhouseWeb.Services.Communication;
using GreenhouseWeb.Services.Incoming;
using System.Threading;
using GreenhouseWeb.Interfaces;

namespace GreenhouseWeb.Services
{
    public class ServicesFacade : IServicesFacadeForServices, IServiceFacade
    {
        private IncomingCommunicator incommingCommunication;
        private LiveData liveData;
        private WatchdogFacade wactchdogFacade;
        private CommunicationFacade communicationFacade;

        public ServicesFacade()
        {
            this.liveData = new LiveData();
        }

        internal void initialise()
        {
            this.wactchdogFacade = new WatchdogFacade(this);
            this.incommingCommunication = new IncomingCommunicator(this);

            incommingCommunication = new IncomingCommunicator(this);
            Thread thread = new Thread(new ThreadStart(incommingCommunication.listenForConnections));
            thread.Start();
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