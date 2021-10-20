using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCModelAndAnnotationAndValidation.Models.ViewModels;

namespace MVCModelAndAnnotationAndValidation.Controllers
{
    public class UserWiseRolesController : Controller
    {
        private EmployeeDBEntities1 db = new EmployeeDBEntities1();

        // GET: UserWiseRoles
        public ActionResult Index()
        {
            var userWiseRoles = db.UserWiseRoles.Include(u => u.TblRole).Include(u => u.TblUser);
            return View(userWiseRoles.ToList());
        }

        // GET: UserWiseRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserWiseRole userWiseRole = db.UserWiseRoles.Find(id);
            if (userWiseRole == null)
            {
                return HttpNotFound();
            }
            return View(userWiseRole);
        }

        // GET: UserWiseRoles/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.TblRoles, "Id", "RoleName_");
            ViewBag.UserId = new SelectList(db.TblUsers, "Id", "UserName");
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,RoleId")] UserWiseRole userWiseRole)
        {
            if (ModelState.IsValid)
            {
                db.UserWiseRoles.Add(userWiseRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.TblRoles, "Id", "RoleName_", userWiseRole.RoleId);
            ViewBag.UserId = new SelectList(db.TblUsers, "Id", "UserName", userWiseRole.UserId);
            return View(userWiseRole);
        }

        // GET: UserWiseRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserWiseRole userWiseRole = db.UserWiseRoles.Find(id);
            if (userWiseRole == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.TblRoles, "Id", "RoleName_", userWiseRole.RoleId);
            ViewBag.UserId = new SelectList(db.TblUsers, "Id", "UserName", userWiseRole.UserId);
            return View(userWiseRole);
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,RoleId")] UserWiseRole userWiseRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userWiseRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.TblRoles, "Id", "RoleName_", userWiseRole.RoleId);
            ViewBag.UserId = new SelectList(db.TblUsers, "Id", "UserName", userWiseRole.UserId);
            return View(userWiseRole);
        }

        // GET: UserWiseRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserWiseRole userWiseRole = db.UserWiseRoles.Find(id);
            if (userWiseRole == null)
            {
                return HttpNotFound();
            }
            return View(userWiseRole);
        }

        // POST: UserWiseRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserWiseRole userWiseRole = db.UserWiseRoles.Find(id);
            db.UserWiseRoles.Remove(userWiseRole);
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
