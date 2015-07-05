using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(DrawTogether.Site.OwinStartup))]

namespace DrawTogether.Site
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}