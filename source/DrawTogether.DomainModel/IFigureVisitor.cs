using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawTogether.DomainModel
{
    public interface IFigureVisitor<TState, TResult>
    {
        TResult Visit(PolygonFigure figure, TState state);
    }
}
