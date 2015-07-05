using DrawTogether.Backend;
using DrawTogether.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrawTogether.Site
{
    class Backend
    {
        static readonly Lazy<ServiceFactory> s_serviceFactory =
            new Lazy<ServiceFactory>(() => new ServiceFactory());
        static readonly Dictionary<string, BackendSession> s_sessions =
            new Dictionary<string, BackendSession>();

        public static ServiceFactory ServiceFactory
        {
            get { return s_serviceFactory.Value; }
        }

        public static IDictionary<string, BackendSession> Sessions
        {
            get { return s_sessions; }
        }
    }

    class BackendSession : IWhiteboardServiceCallback, IUserServiceCallback
    {
        public WhiteboardContract Whiteboard { get; set; }
        public UserContract User { get; set; }

        public void NotifyWhiteboardCreated(WhiteboardContract whiteboard)
        {
        }

        public void NotifyUserAttached(int id, UserContract user)
        {
        }

        public void NotifyUserDetached(int id, int userId)
        {
        }

        public void NotifyFigureAdded(int id, FigureContract figure)
        {
        }

        public void NotifyFigureRemoved(int id, int figureIndex)
        {
        }

        public void NotifyWhiteboardDeleted(int id)
        {
        }
    }
}