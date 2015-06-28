using DrawTogether.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DrawTogether.Backend
{
    class BackendService : IWhiteboardService, IUserService
    {
        static readonly Lazy<BackendService> s_instance =
            new Lazy<BackendService>(() =>
                new BackendService(), isThreadSafe: true);

        readonly ServiceCallbacks Callbacks = new ServiceCallbacks();

        readonly object sync = new object();
        readonly List<Whiteboard> whiteboards = new List<Whiteboard>();
        readonly List<User> registeredUsers = new List<User>();
        int whiteboardId;
        int userId;

        public static BackendService Instance
        {
            get { return s_instance.Value; }
        }

        public void RegisterCallback(IWhiteboardServiceCallback callback)
        {
            Callbacks.RegisterCallback(callback);
        }

        public void RegisterCallback(IUserServiceCallback callback)
        {
            Callbacks.RegisterCallback(callback);
        }

        public void Clear()
        {
            Callbacks.Clear();

            lock (this.sync)
            {
                this.whiteboards.Clear();
                this.registeredUsers.Clear();
            }
        }

        public WhiteboardContract Create(string name)
        {
            var id = Interlocked.Increment(ref this.whiteboardId);
            WhiteboardContract contract;

            lock (this.sync)
            {
                if (GetByName(name) != null)
                    throw new ArgumentException("name must be unique!");

                var whiteboard = new Whiteboard(id, name);
                this.whiteboards.Add(whiteboard);

                contract = Contracts.FromWhiteboard(whiteboard);
            }

            Callbacks.NotifyWhiteboardCreated(contract);
            return contract;
        }

        public WhiteboardContract Get(int id)
        {
            lock (this.sync)
            {
                var whiteboard = this.whiteboards.FirstOrDefault(w => w.Id == id);

                return whiteboard != null
                       ? Contracts.FromWhiteboard(whiteboard)
                       : null;
            }
        }

        public WhiteboardContract GetByName(string name)
        {
            lock (this.sync)
            {
                var whiteboard = this.whiteboards.FirstOrDefault(w => w.Name == name);

                return whiteboard != null
                       ? Contracts.FromWhiteboard(whiteboard)
                       : null;
            }
        }

        public bool Delete(int id)
        {
            bool isDeleted;

            lock (this.sync)
                isDeleted = this.whiteboards.RemoveAll(w => w.Id == id) > 0;

            if (isDeleted)
                Callbacks.NotifyWhiteboardDeleted(id);

            return isDeleted;
        }

        public void AttachUser(int id, int userId)
        {
            User user;

            lock (this.sync)
            {
                var whiteboard = GetWhiteboard(id);

                if (whiteboard == null)
                    return;

                user = GetRegisteredUser(userId);

                if (user == null)
                    return;

                whiteboard.AttachUser(user);
            }

            Callbacks.NotifyUserAttached(id, Contracts.FromUser(user));
        }

        public void AddFigure(int id, FigureContract figureContract)
        {
            Figure figure;

            lock (this.sync)
            {
                var whiteboard = GetWhiteboard(id);

                if (whiteboard == null)
                    return;

                switch (figureContract.Kind)
                {
                    case FigureKindContract.Polygon:
                        figure = new PolygonFigure(
                            GetRegisteredUser(userId),
                            figureContract.Argb,
                            figureContract.Vertices.Select(v => new Vertex(v.X, v.Y)));
                        break;

                    default:
                        return;
                }

                whiteboard.AddFigure(figure);
            }

            Callbacks.NotifyFigureAdded(id, Contracts.FromFigure(figure));
        }

        public void RemoveFigure(int id, int figureIndex)
        {
            lock (this.sync)
            {
                var whiteboard = GetWhiteboard(id);

                if (whiteboard == null)
                    return;

                whiteboard.RemoveFigure(figureIndex);
            }

            Callbacks.NotifyFigureRemoved(id, figureIndex);
        }

        public UserContract RegisterUser(string userName)
        {
            User user;

            lock (this.sync)
            {
                if (this.registeredUsers.Any(u => u.Name == userName))
                    throw new ArgumentException("userName must be unique!");

                user = new User(Interlocked.Increment(ref userId), userName);
                this.registeredUsers.Add(user);
            }

            return Contracts.FromUser(user);
        }

        public bool LogoffUser(int userId)
        {
            lock (this.sync)
                return this.registeredUsers.RemoveAll(u => u.Id == userId) > 0;
        }

        ///////////////////////////////////////////////////////////////////////

        Whiteboard GetWhiteboard(int id)
        {
            lock (this.sync)
                return this.whiteboards.FirstOrDefault(w => w.Id == id);
        }

        User GetRegisteredUser(int id)
        {
            lock (this.sync)
                return this.registeredUsers.FirstOrDefault(u => u.Id == id);
        }
    }
}
