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
            var model = new IndexModel();

            try
            {
            }
            catch
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
    }
}
