using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawTogether.DomainModel
{
    public class PolygonFigure : Figure
    {
        readonly object sync = new object();
        readonly List<Vertex> vertices = new List<Vertex>();

        public PolygonFigure(string userName, Argb color, IEnumerable<Vertex> vertices)
        : base(userName, color)
        {
            Contract.Requires(vertices != null);

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

        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods",
            Justification = "Validated through base class contract")]
        public override TResult Accept<TState, TResult>(IFigureVisitor<TState, TResult> visitor, TState state)
        {
            return visitor.Visit(this, state);
        }
    }
}
