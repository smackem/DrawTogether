using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawTogether.Backend
{
    struct Vertex
    {
        readonly int x;
        readonly int y;

        public Vertex(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X
        {
            get { return this.x; }
        }

        public int Y
        {
            get { return this.y; }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var peer = (Vertex)obj;

            return peer.x == this.x && peer.y == this.y;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() | (Y.GetHashCode() << 16);
        }

        public static bool operator ==(Vertex a, Vertex b)
        {
            return Object.Equals(a, b);
        }

        public static bool operator !=(Vertex a, Vertex b)
        {
            return Object.Equals(a, b) == false;
        }
    }
}
