using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawTogether.DomainModel
{
    public class Whiteboard
    {
        readonly int id;
        readonly List<string> attachedUsers = new List<string>();
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

        public IReadOnlyList<string> AttachedUsers
        {
            get
            {
                lock (this.sync)
                    return this.attachedUsers.ToArray();
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

        public bool AttachUser(string userName)
        {
            Contract.Requires(userName != null);

            lock (this.sync)
            {
                if (this.attachedUsers.Contains(userName) == false)
                {
                    this.attachedUsers.Add(userName);
                    return true;
                }
            }

            return false;
        }

        public bool DetachUser(string userName)
        {
            Contract.Requires(userName != null);

            lock (this.sync)
                return this.attachedUsers.Remove(userName);
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
