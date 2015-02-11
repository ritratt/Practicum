using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using API.Areas.AppManager.Models;

namespace API.Areas.AppManager.Controllers
{
    public class AppsController : Controller
    {
        private AppManagerContext db = new AppManagerContext();

        // GET: AppManager/Apps
        public ActionResult Index()
        {
            return View(db.Apps.ToList());
        }

        // GET: AppManager/Apps/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App apps = db.Apps.Find(id);
            if (apps == null)
            {
                return HttpNotFound();
            }
            return View(apps);
        }

        // GET: AppManager/Apps/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppManager/Apps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(App app)
        {
            String apiKey = Guid.NewGuid().ToString("N");
            app.Id = apiKey;

            if (ModelState.IsValid)
            {
                db.Apps.Add(app);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(app);
        }

        // GET: AppManager/Apps/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App apps = db.Apps.Find(id);
            if (apps == null)
            {
                return HttpNotFound();
            }
            return View(apps);
        }

        // POST: AppManager/Apps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GTAccount")] App apps)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apps).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(apps);
        }

        // GET: AppManager/Apps/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            App apps = db.Apps.Find(id);
            if (apps == null)
            {
                return HttpNotFound();
            }
            return View(apps);
        }

        // POST: AppManager/Apps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            App apps = db.Apps.Find(id);
            db.Apps.Remove(apps);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
