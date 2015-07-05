using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DrawTogether.Site.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View(new Models.LoginModel());
        }

        // POST: Login/Index
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            try
            {
                var userName = collection["UserName"];
                var whiteboardName = collection["WhiteboardName"];

                var session = new BackendSession();
                var users = Backend.ServiceFactory.CreateUserService(session);
                var whiteboards = Backend.ServiceFactory.CreateWhiteboardService(session);

                var user = users.RegisterUser(userName);
                var whiteboard = whiteboards.Create(whiteboardName);

                session.Whiteboard = whiteboard;
                session.User = user;

                Backend.Sessions[userName] = session;

                Session.Add("userName", userName);

                return RedirectToAction("Index", "Draw", new { id = whiteboard.Id });
            }
            catch
            {
                return View();
            }
        }
    }
}
