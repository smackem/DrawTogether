using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrawTogether.Site.ApplicationLayer.Login
{
    public class LoginService
    {
        public int CreateWhiteboard(IndexModel inputModel)
        {
            var whiteboard = Backend.Instance.CreateWhiteboard(inputModel.WhiteboardName, 800, 600);

            return whiteboard.Id;
        }

        public bool WhiteboardExists(string name)
        {
            return Backend.Instance.Whiteboards.Any(wb => wb.Name == name);
        }
    }
}