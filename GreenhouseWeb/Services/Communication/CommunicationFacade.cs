using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GreenhouseWeb.Services.Interfaces;
using Newtonsoft.Json.Linq;

namespace GreenhouseWeb.Services.Communication
{
    public class CommunicationFacade
    {
        private ServicesFacade servicesFacade;

        public CommunicationFacade(ServicesFacade servicesFacade)
        {
            this.servicesFacade = servicesFacade;
        }

        public void RetryConnection(string greenhouseID)
        {

            string greenhouseConnectionInfo = new GreenhouseIPRetriever().GetIP(greenhouseID);
            new Communicator().SendRetryConnection(greenhouseConnectionInfo);

        }

        public void applySchedule(string greenhouseID, JObject schedule)
        {
            string greenhouseConnectionInfo = new GreenhouseIPRetriever().GetIP(greenhouseID);
            new Communicator().applySchedule(greenhouseConnectionInfo, schedule);
        }

        public void getLiveData(string greenhouseID)
        {
            JObject message = new JObject();
            message.Add("procedure", "getLiveData");
            message.Add("IPAddress", "127.0.0.1");
            message.Add("port", 8090);

            string greenhouseConnectionInfo = new GreenhouseIPRetriever().GetIP(greenhouseID);

            new Communicator().getLiveData(greenhouseConnectionInfo, message);
        }

    }
}