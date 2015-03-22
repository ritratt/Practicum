using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using API.Areas.RoleManager.Models;
using API.RoleManager.Models;

namespace API.Areas.RoleManager.Controllers
{
    public class UsersController : Controller
    {
        private RoleManagerContext db = new RoleManagerContext();

        // GET: RoleManager/Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: RoleManager/Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: RoleManager/Users/Create
        public ActionResult Create()
        {
            var user = new User();

            user.Roles = new List<Role>();

            return View(new CreateUserViewModel
            {
                User = user,
                RolesList = db.Roles.ToList().Select(x => new SelectListItem()
                {
                    Selected = user.Roles.Contains(x),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        // POST: RoleManager/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateUserViewModel model, String[] selectedRoles)
        {
            var user = model.User;
            user.Roles= new List<Role>();

            foreach (var selectedRole in selectedRoles)
            {
                var role = db.Roles.FirstOrDefault(r => r.Name == selectedRole);
                user.Roles.Add(role);
            }

            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: RoleManager/Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: RoleManager/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GTAccount")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: RoleManager/Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: RoleManager/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
