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
    public class GroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Groups
        public ActionResult Index()
        {
            var facultyGroupList = db.GetFacultyGroup();
            return View(facultyGroupList.ToList());
        }

        // GET: Groups/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacultyGroup facultyGroup = db.GetFacultyGroup(id);
            if (facultyGroup == null)
            {
                return HttpNotFound();
            }
            return View(facultyGroup);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            List<Faculty> facultyList = db.GetFaculty();
            ViewBag.FacultyID = new SelectList(facultyList, "FacultyID", "FacultyName");
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupID,CreateDate,FacultyID")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.CreateGroup(group);
                return RedirectToAction("Index");
            }

            List<Faculty> facultiesList = db.GetFaculty();

            ViewBag.FacultyID = new SelectList(facultiesList, "FacultyID", "FacultyName", group.FacultyID);
            return View(group);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.GetGroup(id);
            if (group == null)
            {
                return HttpNotFound();
            }

            List<Faculty> facultiesList = db.GetFaculty();
            ViewBag.FacultyID = new SelectList(facultiesList, "FacultyID", "FacultyName", group.FacultyID);
            return View(group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID,CreateDate,FacultyID")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.UpdateGroup(group);
                return RedirectToAction("Index");
            }
 
            ViewBag.FacultyID = new SelectList(db.GetFaculty(), "FacultyID", "FacultyName", group.FacultyID);
            return View(group);
        }

        // GET: Groups/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacultyGroup facultyGroup = db.GetFacultyGroup(id);
            if (facultyGroup == null)
            {
                return HttpNotFound();
            }
            return View(facultyGroup);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            db.DeleteGroup(id);
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
