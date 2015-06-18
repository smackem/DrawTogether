using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawTogether.Backend
{
    static class Extensions
    {
        public static T GetTarget<T>(this WeakReference<T> @this) where T : class
        {
            T target;

            return @this.TryGetTarget(out target)
                   ? target
                   : null;
        }
    }
}
