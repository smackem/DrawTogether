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

            try
            {
                var whiteboard = Backend.Instance.GetWhiteboard(id);

                model = new IndexModel
                {
                    WhiteboardId = id,
                    WhiteboardName = whiteboard.Name,
                    UserName = Session["userName"] as string,
                    WhiteboardWidth = whiteboard.Width,
                    WhiteboardHeight = whiteboard.Height,
                    WhiteboardFigures = whiteboard.Figures.Select(FigureModel.ViewModelFromFigure).ToArray(),
                    AttachedUserNames = whiteboard.AttachedUsers.ToArray(),
                };
            }
            catch
            {
                return RedirectToAction("Index", "Login");
            }

            return View(model);
        }
    }
}
