using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GreenhouseWeb.Services;

namespace GreenhouseWeb.Services
{
    public class IncomingCommunication
    {

        private ServicesFacade servicesFadace;

        public IncomingCommunication(ServicesFacade servicesFacade)
        {
            this.servicesFadace = servicesFacade;
        }


    }
}