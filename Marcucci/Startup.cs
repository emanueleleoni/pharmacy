using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Marcucci.Startup))]
namespace Marcucci
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();

            ConfigureAuth(app);
        }
    }
}
