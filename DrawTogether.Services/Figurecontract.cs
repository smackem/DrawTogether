using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawTogether.Services
{
    public class FigureContract
    {
        public FigureContract(VertexContract[] vertices, FigureKindContract kind, int argb, int userId)
        {
            Vertices = vertices;
            Kind = kind;
            Argb = argb;
            UserId = userId;
        }

        public VertexContract[] Vertices { get; private set; }
        public FigureKindContract Kind { get; private set; }
        public int Argb { get; private set; }
        public int UserId { get; private set; }
    }
}
