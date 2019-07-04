using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentAttendence.Models;

namespace StudentAttendence.Controllers
{
    [Authorize(Roles = "Admin,  Student_Service")]
    public class TeacherRolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TeacherRoles
        public ActionResult Index()
        {
            var teacherRoles = db.GetTeacherRoleModule();
            return View(teacherRoles);
        }

        // GET: TeacherRoles/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherRoleModule teacherRole = db.GetTeacherRoleModule(id);
            if (teacherRole == null)
            {
                return HttpNotFound();
            }
            return View(teacherRole);
        }

        // GET: TeacherRoles/Create
        public ActionResult Create()
        {
            ViewBag.ModuleID = new SelectList(db.GetModule(), "ModuleID", "ModuleName");
            ViewBag.RoleID = new SelectList(db.GetRole(), "RoleID", "RoleName");
            ViewBag.TeacherID = new SelectList(db.GetTeacher(), "TeacherID", "FirstName");
            return View();
        }

        // POST: TeacherRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TeacherID,ModuleID,RoleID")] TeacherRole teacherRole)
        {
            if (ModelState.IsValid)
            {
                db.CreateTeacherRole(teacherRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ModuleID = new SelectList(db.GetModule(), "ModuleID", "ModuleName", teacherRole.ModuleID);
            ViewBag.RoleID = new SelectList(db.GetRole(), "RoleID", "RoleName", teacherRole.RoleID);
            ViewBag.TeacherID = new SelectList(db.GetTeacher(), "TeacherID", "FirstName", teacherRole.TeacherID);
            return View(teacherRole);
        }

        // GET: TeacherRoles/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherRole teacherRole = db.GetTeacherRole(id);
            
            if (teacherRole == null)
            {
                return HttpNotFound();
            }
            ViewBag.ModuleID = new SelectList(db.GetModule(), "ModuleID", "ModuleName", teacherRole.ModuleID);
            
            ViewBag.RoleID = new SelectList(db.GetRole(), "RoleID", "RoleName", teacherRole.RoleID);
            ViewBag.TeacherID = new SelectList(db.GetTeacher(), "TeacherID", "FirstName", teacherRole.TeacherID);
            return View(teacherRole);
        }

        // POST: TeacherRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TeacherID,ModuleID,RoleID")] TeacherRole teacherRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacherRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ModuleID = new SelectList(db.GetModule(), "ModuleID", "ModuleName", teacherRole.ModuleID);
            ViewBag.RoleID = new SelectList(db.GetRole(), "RoleID", "RoleName", teacherRole.RoleID);
            ViewBag.TeacherID = new SelectList(db.GetTeacher(), "TeacherID", "FirstName", teacherRole.TeacherID);
            return View(teacherRole);
        }

        // GET: TeacherRoles/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherRoleModule teacherRole = db.GetTeacherRoleModule(id);
            if (teacherRole == null)
            {
                return HttpNotFound();
            }
            return View(teacherRole);
        }

        // POST: TeacherRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.DeleteTeacherRole(id);
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
