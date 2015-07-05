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
            var model = new Models.DrawModel();

            try
            {
                var userName = (string)Session["userName"];
                var session = Backend.Sessions[userName];
                model.UserName = userName;
                model.WhiteboardId = session.Whiteboard.Id;
            }
            catch
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
    }
}
