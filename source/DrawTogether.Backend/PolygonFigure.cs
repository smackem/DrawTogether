using DrawTogether.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawTogether.Backend
{
    class PolygonFigure : Figure
    {
        readonly object sync = new object();
        readonly List<Vertex> vertices = new List<Vertex>();

        public PolygonFigure(User user, Argb argb, IEnumerable<Vertex> vertices)
        : base(user, argb)
        {
            this.vertices = vertices.ToList();
        }

        public IReadOnlyList<Vertex> Vertices
        {
            get
            {
                lock (this.sync)
                    return this.vertices.ToArray();
            }
        }

        public override TResult Accept<TState, TResult>(IFigureVisitor<TState, TResult> visitor, TState state)
        {
            return visitor.visit(this, state);
        }
    }
}
