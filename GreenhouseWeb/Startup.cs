using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GreenhouseWeb.Startup))]
namespace GreenhouseWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
