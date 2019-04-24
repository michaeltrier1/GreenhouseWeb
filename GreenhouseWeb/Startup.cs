using GreenhouseWeb.Services;
using GreenhouseWeb.Services.Incoming;
using Microsoft.Owin;
using Owin;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Web.Services.Description;

[assembly: OwinStartupAttribute(typeof(GreenhouseWeb.Startup))]
namespace GreenhouseWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            startServices();
        }

        public void ServicesStarter(ServiceCollection serviceCollection)
        {

        }


        private void startServices()
        {
            //IncomingCommunicator communicator = new IncomingCommunicator();
            //Thread thread = new Thread(new ThreadStart(communicator.listenForConnections));
            //thread.Start();
        }
    }
}
