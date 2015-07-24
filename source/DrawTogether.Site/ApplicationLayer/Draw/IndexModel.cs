using DrawTogether.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrawTogether.Site.ApplicationLayer.Draw
{
    public class IndexModel
    {
        public string UserName { get; set; }
        public string WhiteboardName { get; set; }
        public int WhiteboardId { get; set; }
        public string[] AttachedUserNames { get; set; }
        public int WhiteboardWidth { get; set; }
        public int WhiteboardHeight { get; set; }
        public FigureModel[] WhiteboardFigures { get; set; }
    }

    public static class ArgbUtils
    {
        public static string ArgbToString(Argb color)
        {
            return "#000000";
        }

        public static Argb ArgbFromString(string str)
        {
            return Argb.FromArgb(255, 0, 0, 0);
        }
    }
}
