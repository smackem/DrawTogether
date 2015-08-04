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
            var whiteboard = BackEnd.Instance.GetWhiteboard(whiteboardId);

            whiteboard.AttachUser(userName);
        }

        public void DetachUser(int whiteboardId, string userName)
        {
            var whiteboard = BackEnd.Instance.GetWhiteboard(whiteboardId);

            whiteboard.DetachUser(userName);
        }

        public void AddFigure(int whiteboardId, FigureModel figureModel)
        {
            var whiteboard = BackEnd.Instance.GetWhiteboard(whiteboardId);
            var figure = figureModel.ToFigure();

            whiteboard.AddFigure(figure);
        }

        public IEnumerable<FigureModel> GetFigures(int whiteboardId)
        {
            var whiteboard = BackEnd.Instance.GetWhiteboard(whiteboardId);

            return whiteboard.Figures.Select(figure =>
                FigureModel.FromFigure(figure));
        }
    }
}