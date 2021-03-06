﻿using System;
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
        private IIncomingCommunicator incommingCommunication;
        private ILiveDataFacade liveData;
        private IWatchdogFacade wactchdogFacade;
        private ICommunicationFacade communicationFacade;
        private IScheduleFacade scheduleFacade;

        public ServicesFacade()
        {
            this.scheduleFacade = new ScheduleFacade();
        }

        internal void initialise()
        {
            this.communicationFacade = new CommunicationFacade(this);
            this.wactchdogFacade = new WatchdogFacade(this);
            this.incommingCommunication = new IncomingCommunicator(this);
            this.liveData = new LiveData(this);

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
            this.liveData.stopLiveData(greenhouseID);
        }

        public void applySchedule(string greenhouseID, JObject rawSchedule)
        {
            JObject processedSchedule = this.scheduleFacade.repackage(rawSchedule);

            this.communicationFacade.applySchedule(greenhouseID, processedSchedule);
        }

        public void startLiveDataStream(string greenhouseID)
        {
            this.communicationFacade.getLiveData(greenhouseID);
        }

        public void clear()
        {
            incommingCommunication.clear();
            wactchdogFacade.clear();
        }

    }
}