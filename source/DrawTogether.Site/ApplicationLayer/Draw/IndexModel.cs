﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrawTogether.Site.Models
{
    public class DrawModel
    {
        public string UserName { get; set; }
        public string WhiteboardName { get; set; }
        public int WhiteboardId { get; set; }
        public IEnumerable<Tuple<String, int>> AttachedUserRefs { get; set; }
    }
}