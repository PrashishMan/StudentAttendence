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
    public class ModuleGroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ModuleGroups
        public ActionResult Index()
        {
            var moduleGroups = db.GetGroupSemesterModules();
            return View(moduleGroups.ToList());
        }

        // GET: ModuleGroups/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupModulesSemester moduleGroups = db.GetGroupModulesSemester(id);
            if (moduleGroups == null)
            {
                return HttpNotFound();
            }
            return View(moduleGroups);
        }

        // GET: ModuleGroups/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(db.GetGroup(), "GroupID", "GroupID");
            ViewBag.ModuleID = new SelectList(db.GetModule(), "ModuleID", "ModuleName");
            ViewBag.SemesterID = new SelectList(db.GetSemester(), "SemesterID", "SemesterID");
            return View();
        }

        // POST: ModuleGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SemesterID,ModuleID,GroupID")] ModuleGroups moduleGroups)
        {
            if (ModelState.IsValid)
            {
                db.CreateGroupModule(moduleGroups);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

           
            return View(moduleGroups);
        }

        // GET: ModuleGroups/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModuleGroups moduleGroups = db.GetGroupModule(id);
            if (moduleGroups == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(db.GetGroup(), "GroupID", "GroupID", moduleGroups.GroupID);
            ViewBag.ModuleID = new SelectList(db.GetModule(), "ModuleID", "ModuleName", moduleGroups.ModuleID);
            ViewBag.SemesterID = new SelectList(db.GetSemester(), "SemesterID", "SemesterNo", moduleGroups.SemesterID);
            return View(moduleGroups);
        }

        // POST: ModuleGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID ,SemesterID ,ModuleID ,GroupID ")] ModuleGroups moduleGroups)
        {
            if (ModelState.IsValid)
            {
                db.UpdateGroupModule(moduleGroups);
                return RedirectToAction("Index");
            }
            return View(moduleGroups);
        }

        // GET: ModuleGroups/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupModulesSemester moduleGroups = db.GetGroupModulesSemester(id);
            if (moduleGroups == null)
            {
                return HttpNotFound();
            }
            return View(moduleGroups);
        }

        // POST: ModuleGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            db.DeletGroupModule(id);
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
