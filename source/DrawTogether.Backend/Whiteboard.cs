using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawTogether.Backend
{
    class Whiteboard
    {
        readonly int id;
        readonly List<WeakReference<User>> attachedUsers = new List<WeakReference<User>>();
        readonly List<Figure> figures = new List<Figure>();
        readonly object sync = new object();

        public Whiteboard(int id, string name)
        {
            this.id = id;

            Name = name;
        }

        public int Id
        {
            get { return this.id; }
        }

        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public IReadOnlyList<User> AttachedUsers
        {
            get
            {
                lock (this.sync)
                {
                    return this.attachedUsers
                        .Select(userRef => userRef.GetTarget())
                        .Where(user => user != null)
                        .ToArray();
                }
            }
        }

        public IReadOnlyList<Figure> Figures
        {
            get
            {
                lock (this.sync)
                    return this.figures.ToArray();
            }
        }

        public bool AttachUser(User user)
        {
            Contract.Requires(user != null);

            lock (this.sync)
            {
                if (this.attachedUsers.Any(userRef => userRef.GetTarget() == user) == false)
                {
                    this.attachedUsers.Add(new WeakReference<User>(user));
                    return true;
                }
            }

            return false;
        }

        public bool DetachUser(User user)
        {
            Contract.Requires(user != null);

            lock (this.sync)
            {
                return this.attachedUsers.RemoveAll(userRef =>
                    userRef.GetTarget() == user) > 0;
            }
        }

        public void AddFigure(Figure figure)
        {
            Contract.Requires(figure != null);

            lock (this.sync)
                this.figures.Add(figure);
        }

        public void RemoveFigure(int figureIndex)
        {
            Contract.Requires(figureIndex >= 0 && figureIndex < Figures.Count);

            lock (this.sync)
                this.figures.RemoveAt(figureIndex);
        }
    }
}
