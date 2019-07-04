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
    [Authorize(Roles = "Admin,  Student_Service, Teacher")]
    public class GroupTimetablesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GroupTimetables
        public ActionResult Index()
        {
            var groupTimetable = db.GetGroupTimetableBridge();
            return View(groupTimetable.ToList());
        }

        // GET: GroupTimetables/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupTimetableBridge groupTimetable = db.GetGroupTimetableBridge(id);
            if (groupTimetable == null)
            {
                return HttpNotFound();
            }
            return View(groupTimetable);
        }

        // GET: GroupTimetables/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(db.GetGroup(), "GroupID", "GroupID");
            ViewBag.TimetableID = new SelectList((from timetable in db.GetModuleTimetable()
            select new
                                                  {
                                                      TimetableID = timetable.TimeTableId,
                                                      TimetableValue = timetable.ModuleName + " " + timetable.Day + " " + timetable.ClassStartTime + " " + timetable.ClassEndTime
                                                  }), "TimetableID", "TimetableValue");
            return View();
        }

        // POST: GroupTimetables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupID,TimetableID")] GroupTimetable groupTimetable)
        {
            if (ModelState.IsValid)
            {
                db.CreateGroupTimetable(groupTimetable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(db.GetGroup(), "GroupID", "GroupID", groupTimetable.GroupID);
            ViewBag.TimetableID = new SelectList(db.GetTimetable(), "TimeTableId", "Day", groupTimetable.TimetableID);
            return View(groupTimetable);
        }

        // GET: GroupTimetables/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupTimetableBridge groupTimetable = db.GetGroupTimetableBridge(id);
            if (groupTimetable == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(db.GetGroup(), "GroupID", "GroupID", groupTimetable.GroupID);
            ViewBag.TimetableID = new SelectList(db.GetTimetable(), "TimeTableId", "Day", groupTimetable.ID);
            return View(groupTimetable);
        }

        // POST: GroupTimetables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID,TimetableID")] GroupTimetable groupTimetable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groupTimetable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(db.GetGroup(), "GroupID", "GroupID", groupTimetable.GroupID);
            ViewBag.TimetableID = new SelectList(db.GetTimetable(), "TimeTableId", "Day", groupTimetable.TimetableID);
            return View(groupTimetable);
        }

        // GET: GroupTimetables/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupTimetableBridge groupTimetable = db.GetGroupTimetableBridge(id);
            if (groupTimetable == null)
            {
                return HttpNotFound();
            }
            return View(groupTimetable);
        }

        // POST: GroupTimetables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.DeleteGroupTimetable(id);
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
