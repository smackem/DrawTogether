using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrawTogether.Site.ApplicationLayer.LogOn
{
    public class LogOnService
    {
        public int GetOrCreateWhiteboard(IndexModel inputModel)
        {
            var whiteboard = BackEnd.Instance.Whiteboards.FirstOrDefault(wb =>
                string.Compare(wb.Name, inputModel.WhiteboardName, StringComparison.OrdinalIgnoreCase) == 0);

            if (whiteboard == null)
                whiteboard = BackEnd.Instance.CreateWhiteboard(inputModel.WhiteboardName, 800, 600);

            return whiteboard.Id;
        }

        public bool WhiteboardExists(string name)
        {
            return BackEnd.Instance.Whiteboards.Any(wb => wb.Name == name);
        }
    }
}