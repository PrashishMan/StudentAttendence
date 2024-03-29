﻿using System;
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
    public class FacultyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        // GET: Faculty
        public ActionResult Index()
        {

            List<Faculty> facultyList =db.GetFaculty();        
            return View(facultyList);
        }

        // GET: Faculty/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculty faculty = db.GetFaculty(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }
        
        // GET: Faculty/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Faculty/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FacultyID,FacultyName")] Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                db.CreateFaculty(faculty);
                return RedirectToAction("Index");
            }

            return View(faculty);
        }

        // GET: Faculty/Edit/5
        public ActionResult Edit(int id)
        {
            
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculty faculty = db.GetFaculty(id);
                        
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }

        // POST: Faculty/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FacultyID,FacultyName")] Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                db.UpdateFaculty(faculty);                
                return RedirectToAction("Index");
            }
            return View(faculty);
        }

        // GET: Faculty/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Faculty faculty = db.GetFaculty(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }

        // POST: Faculty/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.DeleteFaculty(id);
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
