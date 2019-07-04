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
    public class AttendencesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Attendences
        public ActionResult Index()
        {
            var studentAttendence = db.GetStudentAttendence();
            ViewBag.GroupID = new SelectList(db.GetGroup(), "GroupID", "GroupID");
            ViewBag.TimetableID = new SelectList((from timetable in db.GetModuleTimetable().ToList()
            select new
            {
                TimetableID = timetable.TimeTableId,
                TimetableValue = timetable.ModuleName + " " + timetable.Day + " " + timetable.ClassStartTime + " " + timetable.ClassEndTime
            }),"TimetableID","TimetableValue");

            return View(studentAttendence.ToList());
        }

        public ActionResult FilterIndex(string groupId, int timetableID)
        {
            var studentAttendence = db.GetStudentAttendence(groupId, timetableID);

            ViewBag.GroupID = new SelectList(db.GetGroup(), "GroupID", "GroupID");

            List<ModuleTimetableBridge> timetableList = db.GetModuleTimetable();
            ViewBag.TimetableID = new SelectList((from timetable in timetableList
                                                       select new
                                                  {
                                                      TimetableID = timetable.TimeTableId,
                                                      TimetableValue = timetable.ModuleName + " " + timetable.Day + " " + timetable.ClassStartTime + " " + timetable.ClassEndTime
                                                  }), "TimetableID", "TimetableValue");

            if (timetableList.Count > 0) {
                ViewBag.ModuleName = timetableList[0].ModuleName;
            }            
            ViewBag.GroupName = groupId;
            return View(studentAttendence.ToList());
        }

        // GET: Attendences/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentsAttendence studentAttendence = db.GetStudentAttendence(id);
            if (studentAttendence == null)
            {
                return HttpNotFound();
            }
            return View(studentAttendence);
        }

        

        // GET: Attendences/Create
        public ActionResult Create(string groupID)
        {
            ViewBag.StudentID = new SelectList((from student in db.GetStudent()
                                                select new
                                                {
                                                    StudentID = student.StudentID,
                                                    StudentName = student.FirstName + " " + student.LastName
                                                }), "StudentID", "StudentName");
            ViewBag.TimetableID = new SelectList((from timetable in db.GetModuleTimetable()
                                                  select new
                                                  {
                                                      TimetableID = timetable.TimeTableId,
                                                      TimetableValue = timetable.ModuleName + " " + timetable.Day + " " + timetable.ClassStartTime + " " + timetable.ClassEndTime
                                                  }), "TimetableID", "TimetableValue");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AttendencePage(string groupID, int timetableID) {
            List<Student> studentList = db.GetGroupStudent(groupID);
            Timetable timetable = db.GetTimetable(timetableID);
            Module module = db.GetModule(timetable.ModuleID);
            List<StudentsAttendence> studentsAttendenceList = new List<StudentsAttendence>();
            foreach (Student student in studentList) {
                StudentsAttendence studentAttendence = new StudentsAttendence( student.StudentID, timetable.TimeTableId, student.FirstName, student.LastName, timetable.ClassStartTime, timetable.ClassEndTime, module.ModuleName, DateTime.Now, "p");
                studentsAttendenceList.Add(studentAttendence);
            }
            ViewBag.ModuleName = module.ModuleName;
            ViewBag.ClassStartTime = timetable.ClassStartTime;
            ViewBag.ClassEndTime = timetable.ClassEndTime;
            ViewBag.Date = DateTime.Now;

            return View(studentsAttendenceList);
        }

        // POST: Attendences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( List<StudentsAttendence> studentAttendenceList)
        {
            if (ModelState.IsValid)
            {
                foreach(StudentsAttendence studentAttendence in studentAttendenceList){
                    Attendence attedence = new Attendence(studentAttendence.StudentID, studentAttendence.TimetableID, studentAttendence.Date, studentAttendence.Condition);
                    db.CreateAttendence(attedence);
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("AttendencePage");
        }

        // GET: Attendences/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendence attendence = db.GetAttendence(id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentID = new SelectList((from student in db.GetStudent()
                                                select new
                                                {
                                                    StudentID = student.StudentID,
                                                    StudentName = student.FirstName + " " + student.LastName
                                                }), "StudentID", "StudentName" ,attendence.StudentID);
            ViewBag.TimetableID = new SelectList((from timetable in db.GetModuleTimetable()
                                                  select new
                                                  {
                                                      TimetableID = timetable.TimeTableId,
                                                      TimetableValue = timetable.ModuleName + " " + timetable.Day + " " + timetable.ClassStartTime + " " + timetable.ClassEndTime
                                                  }), "TimetableID", "TimetableValue", attendence.TimetableID);

            return View(attendence);
        }

        // POST: Attendences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AttendenceID,StudentID,TimetableID,Date,Condition")] Attendence attendence)
        {
            if (ModelState.IsValid)
            {
                db.UpdateAttendence(attendence);
                return RedirectToAction("Index");
            }
            return View(attendence);
        }

        // GET: Attendences/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentsAttendence studentAttendence = db.GetStudentAttendence(id);
            if (studentAttendence == null)
            {
                return HttpNotFound();
            }
            return View(studentAttendence);
        }

        // POST: Attendences/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.DeleteAttendence(id);
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
