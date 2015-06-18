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

        public readonly ServiceCallbacks Callbacks =
            new ServiceCallbacks();

        readonly object sync = new object();
        readonly List<Whiteboard> whiteboards = new List<Whiteboard>();
        readonly List<User> registeredUsers = new List<User>();
        int whiteboardId;
        int userId;

        public static BackendService Instance
        {
            get { return s_instance.Value; }
        }

        public WhiteboardContract Create(string name)
        {
            var whiteboard = new Whiteboard(Interlocked.Increment(ref this.whiteboardId), name);
            var contract = Contracts.FromWhiteboard(whiteboard);
            Callbacks.NotifyWhiteboardCreated(contract);

            lock (this.sync)
                this.whiteboards.Add(whiteboard);

            return contract;
        }

        public WhiteboardContract Get(int id)
        {
            lock (this.sync)
            {
                return Contracts.FromWhiteboard(
                    this.whiteboards.FirstOrDefault(w => w.Id == id));
            }
        }

        public bool Delete(int id)
        {
            lock (this.sync)
                return this.whiteboards.RemoveAll(w => w.Id == id) > 0;
        }

        public void AttachUser(int id, int userId)
        {
            lock (this.sync)
            {
                var whiteboard = GetWhiteboard(id);

                if (whiteboard == null)
                    return;

                var user = GetRegisteredUser(userId);

                if (user == null)
                    return;

                whiteboard.AttachUser(user);
            }
        }

        public void AddFigure(int id, FigureContract figureContract)
        {
            lock (this.sync)
            {
                var whiteboard = GetWhiteboard(id);

                if (whiteboard != null)
                {
                    Figure figure;

                    switch (figureContract.Kind)
                    {
                        case FigureKindContract.Polygon:
                            figure = new PolygonFigure(
                                GetRegisteredUser(userId),
                                figureContract.Argb,
                                figureContract.Vertices.Select(v => new Vertex(v.X, v.Y)));
                            break;

                        default:
                            figure = null;
                            break;
                    }

                    if (figure != null)
                        whiteboard.AddFigure(figure);
                }
            }
        }

        public void RemoveFigure(int id, int figureIndex)
        {
            lock (this.sync)
            {
                var whiteboard = GetWhiteboard(id);

                if (whiteboard != null)
                    whiteboard.RemoveFigure(figureIndex);
            }
        }

        public UserContract RegisterUser(string userName)
        {
            throw new NotImplementedException();
        }

        public void LogoffUser(UserContract user)
        {
            throw new NotImplementedException();
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
