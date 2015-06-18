using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DrawTogether.Services
{
    [StructLayout(LayoutKind.Explicit)]
    public struct Argb
    {
        [FieldOffset(0)]
        public readonly byte B;

        [FieldOffset(1)]
        public readonly byte G;

        [FieldOffset(2)]
        public readonly byte R;

        [FieldOffset(3)]
        public readonly byte A;

        [FieldOffset(0)]
        public readonly int Value;

        public static Argb FromArgb(byte a, byte r, byte g, byte b)
        {
            return new Argb(a, r, g, b);
        }

        public static Argb FromArgb(int bgra)
        {
            return new Argb(bgra);
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
