using Owin;
using Microsoft.Owin;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartup(typeof(DrawTogether.Site.OwinStartup))]

namespace DrawTogether.Site
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            var hubConfiguration = new HubConfiguration
            {
                EnableDetailedErrors = true,
            };

            app.MapSignalR(hubConfiguration);
        }
    }
}