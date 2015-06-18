using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawTogether.Services
{
    public class FigureContract
    {
        public static FigureContract FromPolygon(int userId, Argb argb, VertexContract[] vertices)
        {
            return new FigureContract(userId, argb, FigureKindContract.Polygon)
            {
                Vertices = vertices,
            };
        }

        public int UserId { get; private set; }
        public Argb Argb { get; private set; }
        public FigureKindContract Kind { get; private set; }
        public VertexContract[] Vertices { get; private set; }

        ///////////////////////////////////////////////////////////////////////

        FigureContract(int userId, Argb argb, FigureKindContract kind)
        {
            Kind = FigureKindContract.Polygon;
            Argb = argb;
            UserId = userId;
        }
    }
}
