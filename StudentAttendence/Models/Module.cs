using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentAttendence.Models
{
    public class Module
    {
        [Key]
        public int ModuleID { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 5)]
        public string ModuleName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string ModuleType { get; set; }

        [Required]
        public int Credit { get; set; }

        public bool Status { get; set; }

        [ForeignKey("Faculty")]
        public int FacultyID { get; set; }

        [ForeignKey("Semester")]
        public int SemesterID { get; set; }


        public Module(string moduleName, string moduleType, int credit, bool status, int facultyID)
        {
            ModuleName = moduleName;
            ModuleType = moduleType;
            Credit = credit;
            Status = status;
            FacultyID = facultyID;
        }

        public Module(string moduleName, string moduleType, int credit, int facultyID)
        {
            ModuleName = moduleName;
            ModuleType = moduleType;
            Credit = credit;
            FacultyID = facultyID;
            Status = true;
        }

        public Module()
        {
            this.Status = true;
        }

        public virtual Faculty Faculty { get; set; }
        public virtual Semester Semester { get; set; }
        public virtual List<TeacherRole> TeacherRole { get; set; }

    }
}