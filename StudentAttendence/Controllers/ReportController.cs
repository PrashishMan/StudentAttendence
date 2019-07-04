using StudentAttendence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentAttendence.Controllers
{
    [Authorize(Roles = "Admin,  Student_Service, Teacher")]
    public class ReportController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Report
        public ActionResult Index()
        {
            List<SelectListItem> ReportTypeList = new List<SelectListItem> {
                new SelectListItem {
                    Value = "Daily",
                    Text = "Daily"
                },
                new SelectListItem {
                    Value = "Weekly",
                    Text = "Weekly"
                },
                new SelectListItem {
                    Value = "Monthly",
                    Text = "Monthly"
                }

            };

            List<SelectListItem> StudentReportTypeList = new List<SelectListItem> {
                
                new SelectListItem {
                    Value = "Weekly",
                    Text = "Weekly"
                },
                new SelectListItem {
                    Value = "Monthly",
                    Text = "Monthly"
                }

            };

            ViewBag.StudentID = new SelectList((from student in db.GetStudent().ToList()
                                                select new
                                                {
                                                    StudentID = student.StudentID,
                                                    StudentName = student.FirstName + " " + student.LastName
                                                }), "StudentID", "StudentName");


            ViewBag.SemesterID = new SelectList(db.GetSemester(), "SemesterID", "SemesterNo");
            ViewBag.GroupID = new SelectList(db.GetGroup(), "GroupID", "GroupID");
            ViewBag.FacultyID = new SelectList(db.GetFaculty(), "FacultyID", "FacultyName");
            ViewBag.ReportType = new SelectList(ReportTypeList, "Value", "Text");
            ViewBag.StudentReportType = new SelectList(StudentReportTypeList, "Value", "Text");
            ViewBag.AbsentStudents = new SelectList(db.GetTouristStudent(), "Value", "Text");
            return View();
        }

        public ActionResult IndivisualStudentAttendence(DateTime Date, string StudentReportType, int studentID) {
            List<StudentsAttendence> studentsAttendence = new List<StudentsAttendence>();
            switch (StudentReportType)
            {
                case "Weekly":
                    studentsAttendence = db.GetStudentsWeeklyAttendence(Date, studentID);
                    break;

                case "Monthly":
                    studentsAttendence = db.GetStudentsMonthlyAttendence(Date, studentID);
                    break;

            }
            return View(studentsAttendence);
        }

        public ActionResult SemesterTeacherModulePage(int FacultyID, int SemesterID) {
            List<TeacherRoleModule> teacherRoleModules = db.GetSemesterModule(FacultyID, SemesterID);
            Semester semester = db.GetSemester(SemesterID);
            Faculty faculty = db.GetFaculty(FacultyID);
            ViewBag.semesterNo = semester.SemesterNo;
            ViewBag.facultyName = faculty.FacultyName;
            return View(teacherRoleModules);
        }

        public ActionResult GetTeacherTeachingHrs() {
            List<TeacherTeachingHrs> teacherTeachingHrs = db.GetTeacherTeachingHrs();
            return View(teacherTeachingHrs);
        }



        public ActionResult StudentAttendence(DateTime Date, string reportType) {

            List<StudentsAttendence> studentsAttendence = new List<StudentsAttendence>();
            switch (reportType) {
                case "Daily":
                    studentsAttendence = db.GetDailyAttendence(Date);
                    break;

                case "Weekly":
                    studentsAttendence = db.GetWeeklyAttendence(Date);
                    break;

                case "Monthly":
                    studentsAttendence = db.GetMonthlyAttendence(Date);
                    break;


            }
            return View(studentsAttendence);
        }

        public ActionResult TouristStudent() {
            return View(db.GetTouristStudent());
        }

        public ActionResult StudentEnrollment()
        {
            List<Student> students = db.GetStudentByEnrolldate(); 
            return View(students);
        }
    }
}