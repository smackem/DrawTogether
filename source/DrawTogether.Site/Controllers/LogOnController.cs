using DrawTogether.Site.ApplicationLayer.LogOn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DrawTogether.Site.Controllers
{
    public class LogOnController : Controller
    {
        readonly LogOnService service;

        public LogOnController()
        {
            this.service = new LogOnService();
        }

        // GET: LogOn
        public ActionResult Index()
        {
            var model = new IndexModel
            {
                UserName = Session["userName"] as string,
            };

            return View();
        }

        // POST: LogOn/Index
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            var inputModel = new IndexModel
            {
                UserName = collection["UserName"],
                WhiteboardName = collection["WhiteboardName"],
            };

            Session["userName"] = inputModel.UserName;

            try
            {
                var whiteboardId = this.service.GetOrCreateWhiteboard(inputModel);

                return RedirectToAction("Index", "Draw", new { id = whiteboardId });
            }
            catch
            {
                return View();
            }
        }
    }
}
