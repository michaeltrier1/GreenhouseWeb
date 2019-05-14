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
        private IServiceFacade facade;

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
            ServicesFacade facade = new ServicesFacade();
            facade.initialise();
            this.facade = facade;
        }

        public IServiceFacade getFacade()
        {
            return facade;
        }

        public void clear()
        {
            instance = null;
            facade.clear();
            facade = null;
        }

    }
}