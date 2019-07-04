using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentAttendence.Models
{
    public class ModuleTimetableBridge
    {
        public int TimeTableId { get; set; }

        public TimeSpan ClassStartTime { get; set; }

        public TimeSpan ClassEndTime { get; set; }

        public string Day { get; set; }

        public string Room { get; set; }

        public int Year { get; set; }

        public int ModuleID { get; set; }

        public string ModuleName { get; set; }

        public string ClassType { get; set; }

        public string FacultyName { get; set; }

        public int Semester { get; set; }

        public ModuleTimetableBridge()
        {
        }

        public ModuleTimetableBridge(int timeTableId, TimeSpan classStartTime, TimeSpan classEndTime, string day, string room, int year, int moduleID, string moduleName, string facultyName, int semester) : this(timeTableId, classStartTime, classEndTime, day, room, year, moduleID, moduleName, facultyName)
        {
            TimeTableId = timeTableId;
            ClassStartTime = classStartTime;
            ClassEndTime = classEndTime;
            Day = day;
            Room = room;
            Year = year;
            ModuleID = moduleID;
            ModuleName = moduleName;
            FacultyName = facultyName;
            Semester = semester;
        }

        public ModuleTimetableBridge(int timeTableId, TimeSpan classStartTime, TimeSpan classEndTime, string day, string room, int year, int moduleID, string moduleName, string classType, string facultyName, int semester) 
        {
            TimeTableId = timeTableId;
            ClassStartTime = classStartTime;
            ClassEndTime = classEndTime;
            Day = day;
            Room = room;
            Year = year;
            this.ClassType = classType;
            ModuleID = moduleID;
            ModuleName = moduleName;
            FacultyName = facultyName;
            this.Semester = semester;
        }

        public ModuleTimetableBridge(int timeTableId, TimeSpan classStartTime, TimeSpan classEndTime, string day, string room, int year, int moduleID, string moduleName, string facultyName)
        {
            TimeTableId = timeTableId;
            ClassStartTime = classStartTime;
            ClassEndTime = classEndTime;
            Day = day;
            Room = room;
            Year = year;
            ModuleID = moduleID;
            ModuleName = moduleName;
            FacultyName = facultyName;
        }

        public ModuleTimetableBridge(TimeSpan classStartTime, TimeSpan classEndTime, string day, string room, int year, int moduleID, string moduleName, string facultyName)
        {
            ClassStartTime = classStartTime;
            ClassEndTime = classEndTime;
            Day = day;
            Room = room;
            Year = year;
            ModuleID = moduleID;
            ModuleName = moduleName;
            FacultyName = facultyName;
        }
    }
}