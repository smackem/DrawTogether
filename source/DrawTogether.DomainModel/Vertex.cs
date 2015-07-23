using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawTogether.DomainModel
{
    public struct Vertex : IEquatable<Vertex>
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

        public bool Equals(Vertex other)
        {
            return other.x == this.x && other.y == this.y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return Equals((Vertex)obj);
        }

        public override int GetHashCode()
        {
            return x | (y << 16);
        }

        public static bool operator ==(Vertex a, Vertex b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Vertex a, Vertex b)
        {
            return a.Equals(b) == false;
        }
    }
}
