using DrawTogether.DomainModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace DrawTogether.Site.ApplicationLayer.Draw
{
    public class IndexModel
    {
        public string UserName { get; set; }
        public string WhiteboardName { get; set; }
        public int WhiteboardId { get; set; }
        public IReadOnlyList<string> AttachedUserNames { get; set; }
        public int WhiteboardWidth { get; set; }
        public int WhiteboardHeight { get; set; }
        public IReadOnlyList<FigureModel> WhiteboardFigures { get; set; }
    }

    public static class ArgbUtils
    {
        public static string ArgbToString(Argb color)
        {
            return string.Format("#{0:x2}{1:x2}{2:x2}", color.R, color.G, color.B);
        }

        public static Argb ArgbFromString(string str)
        {
            return Argb.FromArgb(255,
                byte.Parse(str.Substring(1, 2), NumberStyles.HexNumber),
                byte.Parse(str.Substring(3, 2), NumberStyles.HexNumber),
                byte.Parse(str.Substring(5, 2), NumberStyles.HexNumber));
        }
    }
}
