using DrawTogether.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace DrawTogether.Site
{
    public class BackEnd
    {
        readonly Dictionary<int, Whiteboard> whiteboards = new Dictionary<int, Whiteboard>();
        readonly object sync = new object();
        int generatedWhiteboardId;

        static readonly BackEnd s_instance = new BackEnd();

        public static BackEnd Instance
        {
            get { return s_instance; }
        }

        public IEnumerable<Whiteboard> Whiteboards
        {
            get
            {
                lock (this.sync)
                    return this.whiteboards.Values.ToArray();
            }
        }

        public Whiteboard GetWhiteboard(int id)
        {
            lock (this.sync)
            {
                Whiteboard whiteboard;

                if (this.whiteboards.TryGetValue(id, out whiteboard))
                    return whiteboard;
            }

            return null;
        }

        public Whiteboard CreateWhiteboard(string name, int width, int height)
        {
            var id = Interlocked.Increment(ref this.generatedWhiteboardId);
            var whiteboard = new Whiteboard(id, name)
            {
                Width = width,
                Height = height,
            };

            lock (this.sync)
                this.whiteboards[id] = whiteboard;

            return whiteboard;
        }
    }
}