using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using DrawTogether.Site.ApplicationLayer.Draw;
using System.Globalization;
using FeatherSharp.ComponentModel;

namespace DrawTogether.Site.Hubs
{
    [Feather(FeatherAction.Log)]
    public class DrawHub : Hub
    {
        readonly DrawService service;

        public DrawHub()
        {
            this.service = new DrawService();
        }

        public override async Task OnConnected()
        {
            var whiteboardId = int.Parse(Context.QueryString["id"], CultureInfo.InvariantCulture);
            var userName = Context.QueryString["user"];

            Log.Trace("whiteboardId={0}, userName={1}", whiteboardId, userName);

            this.service.AttachUser(whiteboardId, userName);

            await ClientsExcept(whiteboardId, Context.ConnectionId).notifyUserAttached(userName);
            await Groups.Add(Context.ConnectionId, whiteboardId.ToString(CultureInfo.InvariantCulture));

            await base.OnConnected();
        }

        public override async Task OnDisconnected(bool stopCalled)
        {
            var whiteboardId = int.Parse(Context.QueryString["id"], CultureInfo.InvariantCulture);
            var userName = Context.QueryString["user"];

            Log.Trace("whiteboardId={0}, userName={1}", whiteboardId, userName);

            this.service.DetachUser(whiteboardId, userName);

            await ClientsExcept(whiteboardId, Context.ConnectionId).notifyUserDetached(userName);

            await base.OnDisconnected(stopCalled);
        }

        public void AddFigure(FigureModel figure)
        {
            var whiteboardId = int.Parse(Context.QueryString["id"], CultureInfo.InvariantCulture);
            var userName = Context.QueryString["user"];

            Log.Trace("whiteboardId={0}, userName={1}", whiteboardId, userName);

            this.service.AddFigure(whiteboardId, figure);

            ClientsExcept(whiteboardId, Context.ConnectionId).notifyFigureAdded(figure);
        }

        public IEnumerable<FigureModel> GetFigures()
        {
            var whiteboardId = int.Parse(Context.QueryString["id"], CultureInfo.InvariantCulture);
            var userName = Context.QueryString["user"];

            Log.Trace("whiteboardId={0}, userName={1}", whiteboardId, userName);

            var figures = this.service.GetFigures(whiteboardId);
            return figures;
        }

        ///////////////////////////////////////////////////////////////////////

        static dynamic ClientsExcept(int whiteboardId, string excludedConnectionId)
        {
            return GlobalHost.ConnectionManager.GetHubContext<DrawHub>().Clients
                .Groups(new[] { whiteboardId.ToString(CultureInfo.InvariantCulture) }, excludedConnectionId);
        }
   }
}