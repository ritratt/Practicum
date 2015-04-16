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
    public class RolesController : Controller
    {
        private RoleManagerContext db = new RoleManagerContext();

        // GET: RoleManager/Roles
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }

        // GET: RoleManager/Roles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: RoleManager/Roles/Create
        public ActionResult Create()
        {
            var role = new Role();
           
            role.Permissions = new List<Permission>();

            return View(new CreateRoleViewModel
            {
                Role = role,
                PermissionsList = db.Permissions.ToList().Select(x => new SelectListItem()
                {
                    Selected = role.Permissions.Contains(x),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        // POST: RoleManager/Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateRoleViewModel model, String[] SelectedPermissions)
        {
            var role = model.Role;
            
            role.Permissions = new List<Permission>();

            foreach (var p in SelectedPermissions)
            {
                var permission = db.Permissions.FirstOrDefault(perm => perm.Name == p);
                role.Permissions.Add(permission);
            }

            if (ModelState.IsValid)
            {
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: RoleManager/Roles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);

            if (role == null)
            {
                return HttpNotFound();
            }

            return View(new CreateRoleViewModel
            {
                Role = role,
                PermissionsList = db.Permissions.ToList().Select(x => new SelectListItem()
                {
                    Selected = role.Permissions.Contains(x),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        // POST: RoleManager/Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateRoleViewModel model, String[] selectedPermissions)
        {
            var role = model.Role;

            role.Permissions = new List<Permission>();

            foreach (var s in selectedPermissions)
            {
                var permission = db.Permissions
                    .FirstOrDefault(p => p.Name == s);
                role.Permissions.Add(permission);
            }

            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: RoleManager/Roles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: RoleManager/Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Role role = db.Roles.Find(id);
            db.Roles.Remove(role);
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
