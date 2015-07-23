﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using DrawTogether.Site.ApplicationLayer.Draw;

namespace DrawTogether.Site.Hubs
{
    public class DrawHub : Hub
    {
        readonly DrawService service;

        public DrawHub()
        {
            this.service = new DrawService();
        }

        public override async Task OnConnected()
        {
            var whiteboardIdStr = Context.QueryString["whiteboardId"];
            var userName = Context.QueryString["userName"];
            int whiteboardId;

            if (Int32.TryParse(whiteboardIdStr, out whiteboardId))
            {
                this.service.AttachUser(whiteboardId, userName);

                await ClientsExcept(whiteboardId, Context.ConnectionId).notifyUserAttached(userName);
                await Groups.Add(Context.ConnectionId, whiteboardIdStr);
            }

            await base.OnConnected();
        }

        public override async Task OnDisconnected(bool stopCalled)
        {
            var whiteboardIdStr = Context.QueryString["whiteboardId"];
            var userName = Context.QueryString["userName"];

            await Groups.Remove(Context.ConnectionId, whiteboardIdStr);

            int whiteboardId;

            if (Int32.TryParse(whiteboardIdStr, out whiteboardId))
            {
                this.service.DetachUser(whiteboardId, userName);

                await ClientsExcept(whiteboardId, Context.ConnectionId).notifyUserAttached(userName);
            }

            await base.OnDisconnected(stopCalled);
        }

        public async Task AddFigure(FigureModel figure)
        {
            var whiteboardIdStr = Context.QueryString["whiteboardId"];
            var userName = Context.QueryString["userName"];

            int whiteboardId;

            if (Int32.TryParse(whiteboardIdStr, out whiteboardId))
            {
                this.service.AddFigure(whiteboardId, figure);

                await ClientsExcept(whiteboardId, Context.ConnectionId).notifyUserAttached(userName);
            }
        }

        ///////////////////////////////////////////////////////////////////////

        static dynamic ClientsExcept(int whiteboardId, string excludedConnectionId)
        {
            return GlobalHost.ConnectionManager.GetHubContext<DrawHub>().Clients
                .Groups(new[] { whiteboardId.ToString() }, excludedConnectionId);
        }
    }
}