using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentAttendence.Models
{
    public class Semester
    {
        [Key]
        public int SemesterID { get; set; }

        public DateTime SemesterStartDate { get; set; }

        public DateTime SemesterEndDate { get; set; }

        public int SemesterNo { get; set; }

        public bool Status { get; set; }

        public Semester()
        {
            Status = true;
        }

        public Semester(DateTime semesterStartDate, DateTime semesterEndDate, int semesterNo)
        {
            SemesterStartDate = semesterStartDate;
            SemesterEndDate = semesterEndDate;
            SemesterNo = semesterNo;
            Status = true;
        }
    }
}