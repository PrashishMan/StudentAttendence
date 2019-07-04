using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentAttendence.Models
{
    public class StudentsAttendence
    {
        public int ID { get; set; }
        public int StudentID { get; set; }

        public int TimetableID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public TimeSpan ClassStartTime { get; set; }
        public TimeSpan ClassEndTime { get; set; }

        public string ModuleName { get; set; }

        public DateTime Date { get; set; }
        public string Condition { get; set; }

        public StudentsAttendence()
        {
        }

        public StudentsAttendence(string firstName, string lastName, TimeSpan classStartTime, TimeSpan classEndTime, string moduleName, DateTime date, string condition)
        {
            FirstName = firstName;
            LastName = lastName;
            ClassStartTime = classStartTime;
            ClassEndTime = classEndTime;
            ModuleName = moduleName;
            Date = date;
            Condition = condition;
        }

        public StudentsAttendence(int studentID, int timetableID, string firstName, string lastName, TimeSpan classStartTime, TimeSpan classEndTime, string moduleName, DateTime date, string condition)
        {
            StudentID = studentID;
            TimetableID = timetableID;
            FirstName = firstName;
            LastName = lastName;
            ClassStartTime = classStartTime;
            ClassEndTime = classEndTime;
            ModuleName = moduleName;
            Date = date;
            Condition = condition;
        }
    }
}