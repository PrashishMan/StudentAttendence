using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentAttendence.Models
{
    public class Timetable
    {
        [Key]
        public int TimeTableId { get; set; }

        [Required]
        public TimeSpan ClassStartTime { get; set; }

        [Required]
        public TimeSpan ClassEndTime { get; set; }

        [Required]
        public string Day { get; set; }

        [Required]
        public string Room { get; set; }

        public bool Status { get; set; }

        public int Year { get; set; }

        public string ClassType { get; set;}

        
        [ForeignKey("Module")]
        public int ModuleID { get; set; }

        [ForeignKey("Semester")]
        public int SemesterID { get; set; }

        public Timetable()
        {
            this.Status = true;
        }

        public Timetable(TimeSpan classStartTime, TimeSpan classEndTime, string day, string room, bool status, int year, int semester, int moduleID)
        {
            ClassStartTime = classStartTime;
            ClassEndTime = classEndTime;
            Day = day;
            Room = room;
            Status = status;
            Year = year;
            SemesterID = semester;
            ModuleID = moduleID;
        }

        public Timetable(int timeTableId, TimeSpan classStartTime, TimeSpan classEndTime, string day, string room, int Year, int ModuleID,bool status)
        {
            TimeTableId = timeTableId;
            ClassStartTime = classStartTime;
            ClassEndTime = classEndTime;
            Day = day;
            Room = room;
            Status = status;
            this.Year = Year;
            this.ModuleID = ModuleID;

        }

        public Timetable(TimeSpan classStartTime, TimeSpan classEndTime, string day, string room,int Year, int ModuleID)
        {
            ClassStartTime = classStartTime;
            ClassEndTime = classEndTime;
            Day = day;
            Room = room;
            this.Year = Year;
            this.ModuleID = ModuleID;
            Status = true;
        }


        public virtual Module Module { get; set; } 
        public virtual Semester Semester { get; set; }
        public virtual List<GroupTimetable> GroupTimetable { get; set; }

    }
}