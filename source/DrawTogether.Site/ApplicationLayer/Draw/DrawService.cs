using DrawTogether.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrawTogether.Site.ApplicationLayer.Draw
{
    public class DrawService
    {
        public void AttachUser(int whiteboardId, string userName)
        {
            var whiteboard = Backend.Instance.GetWhiteboard(whiteboardId);

            whiteboard.AttachUser(userName);
        }

        public void DetachUser(int whiteboardId, string userName)
        {
            var whiteboard = Backend.Instance.GetWhiteboard(whiteboardId);

            whiteboard.DetachUser(userName);
        }

        public void AddFigure(int whiteboardId, FigureModel figureModel)
        {
            var whiteboard = Backend.Instance.GetWhiteboard(whiteboardId);
            var figure = FigureModel.FigureFromViewModel(figureModel);

            whiteboard.AddFigure(figure);
        }
    }
}