using FeatherSharp.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DrawTogether.Site
{
    [Feather(FeatherAction.Log)]
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Log.MessageRaised += Log_MessageRaised;
        }

        void Log_MessageRaised(object sender, LogEventArgs e)
        {
            Debug.Print("{0}@{1}::{2}: {3}", e.Level, e.TypeName, e.MethodName, e.Text);
        }

        void Session_Start(object sender, EventArgs e)
        {
        }

        void Session_End(object sender, EventArgs e)
        {
        }
    }
}
