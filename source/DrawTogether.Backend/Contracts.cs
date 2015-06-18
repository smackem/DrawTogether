using DrawTogether.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawTogether.Backend
{
    static class Contracts
    {
        public static WhiteboardContract FromWhiteboard(Whiteboard whiteboard)
        {
            return new WhiteboardContract(whiteboard.Id, whiteboard.Name,
                whiteboard.Width, whiteboard.Height,
                whiteboard.AttachedUsers.Select(au => au.Id).ToArray(),
                whiteboard.Figures.Select(f => FromFigure(f)).ToArray());
        }

        public static UserContract FromUser(User user)
        {
            return new UserContract(user.Id, user.Name);
        }

        public static FigureContract FromFigure(Figure figure)
        {
            return figure.Accept(FigureContractCreator.Instance, null);
        }

        public static VertexContract FromVertex(Vertex vertex)
        {
            return new VertexContract(vertex.X, vertex.Y);
        }

        ///////////////////////////////////////////////////////////////////////

        class FigureContractCreator : IFigureVisitor<object, FigureContract>
        {
            public static readonly FigureContractCreator Instance = new FigureContractCreator();

            public FigureContract visit(PolygonFigure figure, object state)
            {
                var user = figure.User;
                return FigureContract.FromPolygon(
                    user != null ? user.Id : 0,
                    figure.Argb,
                    figure.Vertices.Select(v => FromVertex(v)).ToArray());
            }
        }
    }
}
