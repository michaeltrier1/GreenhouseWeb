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
using Newtonsoft.Json.Linq;
using GreenhouseWeb.Services.Schedule;

namespace GreenhouseWeb.Services
{
    public class ServicesFacade : IServicesFacadeForServices, IServiceFacade
    {
        private IncomingCommunicator incommingCommunication;
        private LiveData liveData;
        private WatchdogFacade wactchdogFacade;
        private CommunicationFacade communicationFacade;
        private ScheduleFacade scheduleFacade;

        public ServicesFacade()
        {
            this.liveData = new LiveData();
            this.scheduleFacade = new ScheduleFacade();
        }

        internal void initialise()
        {
            this.communicationFacade = new CommunicationFacade(this);
            this.wactchdogFacade = new WatchdogFacade(this);
            this.incommingCommunication = new IncomingCommunicator(this);

            incommingCommunication = new IncomingCommunicator(this);
            Thread thread = new Thread(new ThreadStart(incommingCommunication.listenForConnections));
            thread.Start();
        }

        public IMeasurement getCurrentLiveData(String greenhouseID)
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

        public void stopLiveData(string greenhouseID)
        {
            this.incommingCommunication.stopLiveData(greenhouseID);
        }

        public void applySchedule(string greenhouseID, JObject rawSchedule)
        {
            JObject processedSchedule = this.scheduleFacade.repackage(rawSchedule);

            this.communicationFacade.applySchedule(greenhouseID, processedSchedule);
        }
    }
}