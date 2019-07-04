using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentAttendence.Models
{
    public class Group
    {
        [Key]
        [StringLength(10, MinimumLength = 2)]
        public string GroupID { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public bool Status { get; set; }

        [Required]
        [ForeignKey("Faculty")]
        public int FacultyID { get; set; }
        
        public virtual Faculty Faculty { get; set; }

        public virtual List<Student> Student { get; set; }

        public virtual List<GroupTimetable> GroupTimetable { get; set; }

        public Group()
        {
            this.Status = true;
        }

        public Group(string groupID, DateTime createDate, int facultyID)
        {
            GroupID = groupID;
            CreateDate = createDate;
            FacultyID = facultyID;
            Status = true;
        }

        public Group(DateTime createDate, int facultyID, Faculty faculty)
        {
            CreateDate = createDate;
            FacultyID = facultyID;
            Faculty = faculty;
            Status = true;
        }

        public Group(string groupID, DateTime createDate, bool status, int facultyID, Faculty faculty, List<Student> student, List<GroupTimetable> groupTimetable)
        {
            GroupID = groupID;
            CreateDate = createDate;
            Status = true;
            FacultyID = facultyID;
            Faculty = faculty;
            Student = student;
            GroupTimetable = groupTimetable;
        }
    }
}