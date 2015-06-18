using DrawTogether.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawTogether.Backend
{
    class ServiceCallbacks : IWhiteboardServiceCallback, IUserServiceCallback
    {
        readonly object sync = new object();
        readonly ISet<IWhiteboardServiceCallback> whiteboardCallbacks =
            new HashSet<IWhiteboardServiceCallback>();
        readonly ISet<IUserServiceCallback> userCallbacks =
            new HashSet<IUserServiceCallback>();

        public void RegisterCallback(IWhiteboardServiceCallback callback)
        {
            lock (this.sync)
                this.whiteboardCallbacks.Add(callback);
        }

        public void RegisterCallback(IUserServiceCallback callback)
        {
            lock (this.sync)
                this.userCallbacks.Add(callback);
        }

        public void NotifyWhiteboardCreated(WhiteboardContract whiteboard)
        {
            foreach (var callback in GetWhiteboardCallbacks())
                callback.NotifyWhiteboardCreated(whiteboard);
        }

        public void NotifyUserAttached(int id, UserContract user)
        {
            foreach (var callback in GetWhiteboardCallbacks())
                callback.NotifyUserAttached(id, user);
        }

        public void NotifyUserDetached(int id, int userId)
        {
            foreach (var callback in GetWhiteboardCallbacks())
                callback.NotifyUserDetached(id, userId);
        }

        public void NotifyFigureAdded(int id, FigureContract figure)
        {
            foreach (var callback in GetWhiteboardCallbacks())
                callback.NotifyFigureAdded(id, figure);
        }

        public void NotifyFigureRemoved(int id, int figureIndex)
        {
            foreach (var callback in GetWhiteboardCallbacks())
                callback.NotifyFigureRemoved(id, figureIndex);
        }

        public void NotifyWhiteboardDeleted(int id)
        {
            foreach (var callback in GetWhiteboardCallbacks())
                callback.NotifyWhiteboardDeleted(id);
        }

        ///////////////////////////////////////////////////////////////////////

        IWhiteboardServiceCallback[] GetWhiteboardCallbacks()
        {
            lock (this.sync)
                return this.whiteboardCallbacks.ToArray();
        }

        IUserServiceCallback[] GetUserCallbacks()
        {
            lock (this.sync)
                return this.userCallbacks.ToArray();
        }
    }
}
