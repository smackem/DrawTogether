using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DrawTogether.DomainModel
{
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Argb",
        Justification = "PascalCasification of the technical term ARGB")]
    [StructLayout(LayoutKind.Explicit)]
    public struct Argb : IEquatable<Argb>
    {
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Member is read-only")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Common Term")]
        [FieldOffset(0)]
        public readonly byte B;

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Member is read-only")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Common Term")]
        [FieldOffset(1)]
        public readonly byte G;

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Member is read-only")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Common Term")]
        [FieldOffset(2)]
        public readonly byte R;

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Member is read-only")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Common Term")]
        [FieldOffset(3)]
        public readonly byte A;

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Member is read-only")]
        [FieldOffset(0)]
        public readonly int Value;

        public static readonly Argb Empty = FromArgb(0);

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "a")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "r")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "g")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "b")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Argb")]
        public static Argb FromArgb(byte a, byte r, byte g, byte b)
        {
            return new Argb(a, r, g, b);
        }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Argb")]
        public static Argb FromArgb(int value)
        {
            return new Argb(value);
        }

        public bool Equals(Argb other)
        {
            return other.Value == this.Value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return Equals((Argb)obj);
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public static bool operator ==(Argb one, Argb two)
        {
            return one.Equals(two);
        }

        public static bool operator !=(Argb one, Argb two)
        {
            return one.Equals(two) == false;
        }

        /// <summary>
        /// Gets the color intensity from 0.0 through 1.0.
        /// </summary>
        public double Intensity
        {
            get { return (0.299 * R + 0.587 * G + 0.114 * B) / 255.0; }
        }

        ///////////////////////////////////////////////////////////////////////

        Argb(byte a, byte r, byte g, byte b)
        {
            this.Value = 0;

            this.A = a;
            this.R = r;
            this.G = g;
            this.B = b;
        }

        Argb(int argb)
        {
            this.A = 0;
            this.R = 0;
            this.G = 0;
            this.B = 0;

            this.Value = argb;
        }
    }
}
