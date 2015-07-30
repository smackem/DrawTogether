﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawTogether.DomainModel
{
    public struct Vertex : IEquatable<Vertex>
    {
        readonly int x;
        readonly int y;

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Justification = "Common Term")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y", Justification = "Common Term")]
        public Vertex(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Common Term")]
        public int X
        {
            get { return this.x; }
        }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Common Term")]
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
            return this.x | (this.y << 16);
        }

        public static bool operator ==(Vertex one, Vertex two)
        {
            return one.Equals(two);
        }

        public static bool operator !=(Vertex one, Vertex two)
        {
            return one.Equals(two) == false;
        }
    }
}
