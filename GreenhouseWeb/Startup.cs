using GreenhouseWeb.Services;
using Microsoft.Owin;
using Owin;
using System;
using System.Threading;

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


        private void startServices()
        {
            ServiceFacadeGetter.getInstance().initialiseServices();
        }
    }
}
