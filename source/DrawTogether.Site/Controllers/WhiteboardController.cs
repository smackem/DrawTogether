using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DrawTogether.Site.Controllers
{
    public class WhiteboardController : Controller
    {
        // GET: Whiteboard
        public ActionResult Index()
        {
            return View(Models.Root.Whiteboards);
        }

        // GET: Whiteboard/Details/5
        public ActionResult Details(string name)
        {
            return View(Models.Root.Whiteboards.First(wb => wb.Name == name));
        }

        // GET: Whiteboard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Whiteboard/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Models.Root.Whiteboards.Add(
                    new Model.Whiteboard(collection["Name"])
                    {
                        Width = Int32.Parse(collection["Width"]),
                        Height = Int32.Parse(collection["Height"]),
                    });

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Whiteboard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Whiteboard/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Whiteboard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Whiteboard/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
