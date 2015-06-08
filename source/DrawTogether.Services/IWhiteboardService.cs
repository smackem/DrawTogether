using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawTogether.Services
{
    public interface IWhiteboardService
    {
        WhiteboardContract Get(int id);
        WhiteboardContract Create(string name);
        bool Delete(int id);
        void AttachUser(int id, int userId);
        void AddFigure(int id, FigureContract figure);
    }

    public interface IWhiteboardServiceCallback
    {
        void NotifyWhiteboardCreated(WhiteboardContract whiteboard);
        void NotifyWhiteboardUserAttached(WhiteboardContract whiteboard, UserContract user);
        void NotifyWhiteboardDeleted(int id);
    }
}