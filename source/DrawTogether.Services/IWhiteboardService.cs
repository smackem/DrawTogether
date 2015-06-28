using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawTogether.Services
{
    public interface IWhiteboardService
    {
        WhiteboardContract Create(string name);
        WhiteboardContract Get(int id);
        WhiteboardContract GetByName(string name);
        bool Delete(int id);
        void AttachUser(int id, int userId);
        void AddFigure(int id, FigureContract figure);
        void RemoveFigure(int id, int figureIndex);
    }

    public interface IWhiteboardServiceCallback
    {
        void NotifyWhiteboardCreated(WhiteboardContract whiteboard);
        void NotifyUserAttached(int id, UserContract user);
        void NotifyUserDetached(int id, int userId);
        void NotifyFigureAdded(int id, FigureContract figure);
        void NotifyFigureRemoved(int id, int figureIndex);
        void NotifyWhiteboardDeleted(int id);
    }
}