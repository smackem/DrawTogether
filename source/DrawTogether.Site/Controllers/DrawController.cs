using DrawTogether.Site.ApplicationLayer.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DrawTogether.Site.Controllers
{
    public class DrawController : Controller
    {
        // GET: Draw
        public ActionResult Index(int id)
        {
            var model = null as IndexModel;
            var whiteboard = BackEnd.Instance.GetWhiteboard(id);

            if (whiteboard != null)
            {
                model = new IndexModel
                {
                    WhiteboardId = id,
                    WhiteboardName = whiteboard.Name,
                    UserName = Session["userName"] as string,
                    WhiteboardWidth = whiteboard.Width,
                    WhiteboardHeight = whiteboard.Height,
                    WhiteboardFigures = whiteboard.Figures.Select(FigureModel.FromFigure).ToArray(),
                    AttachedUserNames = whiteboard.AttachedUsers.ToArray(),
                };

                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "LogOn");
            }
        }
    }
}
