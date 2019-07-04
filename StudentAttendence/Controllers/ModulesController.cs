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
    public class ModulesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Modules
        public ActionResult Index()
        {
            var module = db.GetFacultyModule();
            return View(module.ToList());
        }

        // GET: Modules/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacultyModule facultyModule = db.GetFacultyModule(id);
            List<TeacherRoleModule> teacherModuleList = db.GetModuleTeachers(id);
            ViewBag.FacultyName = facultyModule.FacultyName;
            ViewBag.ModuleName = facultyModule.ModuleName;
            ViewBag.ModuleType = facultyModule.ModuleType;
            ViewBag.FacultyID = facultyModule.FacultyID;

            if (facultyModule == null)
            {
                return HttpNotFound();
            }
            return View(teacherModuleList);
        }

        // GET: Modules/Create
        public ActionResult Create()
        {
            ViewBag.FacultyID = new SelectList(db.GetFaculty(), "FacultyID", "FacultyName");
            ViewBag.SemesterID = new SelectList(db.GetSemester(), "SemesterID", "SemesterNo");
            return View();
        }

        // POST: Modules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ModuleID,SemesterID,ModuleName,ModuleType,Credit,Status,FacultyID")] Module module)
        {
            if (ModelState.IsValid)
            {
                db.CreateModule(module);
                return RedirectToAction("Index");
            }

            ViewBag.FacultyID = new SelectList(db.GetFaculty(), "FacultyID", "FacultyName", module.FacultyID);
            return View(module);
        }

        // GET: Modules/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.GetModule(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            ViewBag.FacultyID = new SelectList(db.GetFaculty(), "FacultyID", "FacultyName", module.FacultyID);
            ViewBag.SemesterID = new SelectList(db.GetSemester(), "SemesterID", "SemesterNo");
            return View(module);
        }

        // POST: Modules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ModuleID,ModuleName,ModuleType,Credit,Status,FacultyID")] Module module)
        {
            if (ModelState.IsValid)
            {
                db.UpdateModule(module);
                return RedirectToAction("Index");
            }
            ViewBag.FacultyID = new SelectList(db.GetFaculty(), "FacultyID", "FacultyName", module.FacultyID);
            return View(module);
        }

        // GET: Modules/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacultyModule facultyModule = db.GetFacultyModule(id);
            if (facultyModule == null)
            {
                return HttpNotFound();
            }
            return View(facultyModule);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.DeleteModule(id);
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
