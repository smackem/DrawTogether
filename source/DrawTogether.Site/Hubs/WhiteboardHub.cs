using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace DrawTogether.Site.Hubs
{
    public class WhiteboardHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello("hopp");
        }
    }
}