using GreenhouseWeb.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenhouseWeb.Services
{
    public class ServiceFacadeGetter
    {
        private static ServiceFacadeGetter instance;
        private ServicesFacade facade;

        private ServiceFacadeGetter()
        {
        }

        public static ServiceFacadeGetter getInstance()
        {
            if (instance == null)
            {
                instance = new ServiceFacadeGetter();

                instance.initialiseServices();
            }
            return instance;
        }

        public void initialiseServices()
        {
            facade = new ServicesFacade();
            facade.initialise();
        }

        public IServiceFacade getFacade()
        {
            return facade;
        }

    }
}