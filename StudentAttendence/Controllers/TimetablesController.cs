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
    [Authorize(Roles = "Admin, Student_Service")]
    public class TimetablesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Timetables
        public ActionResult Index()
        {
            var timetable = db.GetModuleTimetable();
            return View(timetable.ToList());
        }

        // GET: Timetables/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModuleTimetableBridge moduleTimetable = db.GetModuleTimetable(id);
            if (moduleTimetable == null)
            {
                return HttpNotFound();
            }
            return View(moduleTimetable);
        }

        // GET: Timetables/Create
        public ActionResult Create()
        {
            List<SelectListItem> ClassTypes = new List<SelectListItem> {
                new SelectListItem(){Text="Tutor", Value="Tutor"},
                new SelectListItem(){Text="Lecturer", Value="Lecturer"}
            };

            ViewBag.ModuleID = new SelectList(db.GetModule(), "ModuleID", "ModuleName");
            ViewBag.SemesterID = new SelectList(db.GetSemester(), "SemesterID", "SemesterNo");
            ViewBag.ClassType = new SelectList(ClassTypes, "Text","Value" );

            return View();
        }

        // POST: Timetables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TimeTableId,ClassStartTime,ClassEndTime,Day,Room,Status,Year,ModuleID, SemesterID, ClassType")] Timetable timetable)
        {
            if (ModelState.IsValid)
            {
                db.CreateTimetable(timetable);
                return RedirectToAction("Index");
            }

            ViewBag.ModuleID = new SelectList(db.GetModule(), "ModuleID", "ModuleName", timetable.ModuleID);
            return View(timetable);
        }

        // GET: Timetables/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timetable timetable = db.GetTimetable(id);
            if (timetable == null)
            {
                return HttpNotFound();
            }

            List<SelectListItem> ClassTypes = new List<SelectListItem> {
                new SelectListItem(){Text="Tutor", Value="Tutor"},
                new SelectListItem(){Text="Lecturer", Value="Lecturer"}
            };
            ViewBag.ClassType = new SelectList(ClassTypes, "Text", "Value");
            ViewBag.ModuleID = new SelectList(db.GetModule(), "ModuleID", "ModuleName", timetable.ModuleID);
            ViewBag.SemesterID = new SelectList(db.GetSemester(), "SemesterID", "SemesterNo", timetable.SemesterID);
            return View(timetable);
        }

        // POST: Timetables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TimeTableId,ClassStartTime,ClassEndTime,Day,Room,Status,Year, ClassType, ModuleID, SemesterID")] Timetable timetable)
        {
            if (ModelState.IsValid)
            {
                db.UpdateTimetable(timetable);
                return RedirectToAction("Index");
            }
            ViewBag.ModuleID = new SelectList(db.GetModule(), "ModuleID", "ModuleName", timetable.ModuleID);
            return View(timetable);
        }

        // GET: Timetables/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModuleTimetableBridge moduleTimetable = db.GetModuleTimetable(id);
            if (moduleTimetable == null)
            {
                return HttpNotFound();
            }
            return View(moduleTimetable);
        }

        // POST: Timetables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.DeleteTimetable(id);
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
