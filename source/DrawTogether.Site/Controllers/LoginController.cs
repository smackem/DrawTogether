using DrawTogether.Site.ApplicationLayer.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DrawTogether.Site.Controllers
{
    public class LoginController : Controller
    {
        public LoginController()
        {
            this.service = new LoginService();
        }

        // GET: Login
        public ActionResult Index()
        {
            var model = new IndexModel
            {
                UserName = Session["userName"] as string,
            };

            return View();
        }

        // POST: Login/Index
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            try
            {
                var inputModel = new IndexModel
                {
                    UserName = collection["UserName"],
                    WhiteboardName = collection["WhiteboardName"],
                };

                Session.Add("userName", inputModel.UserName);

                var whiteboardId = this.service.CreateWhiteboard(inputModel);

                return RedirectToAction("Index", "Draw", new { id = whiteboardId });
            }
            catch
            {
                return View();
            }
        }

        ///////////////////////////////////////////////////////////////////////

        readonly LoginService service;
    }
}
