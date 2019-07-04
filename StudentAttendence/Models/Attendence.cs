using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentAttendence.Models
{
    public class Attendence
    {
        
        [Key]
        public int AttendenceID { get; set; }

        [ForeignKey("Student")]
        public int StudentID { get; set; }

        
        [ForeignKey("Timetable")]
        public int TimetableID { get; set; }
        
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Condition { get; set; }

        public Attendence() { }

        public Attendence(int studentID, int timetableID, DateTime date, string condition)
        {
            StudentID = studentID;
            TimetableID = timetableID;
            Date = date;
            Condition = condition;
        }

        public virtual Timetable Timetable { get; set; }
        public virtual Student Student { get; set; }
    }
}