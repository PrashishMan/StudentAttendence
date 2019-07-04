using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentAttendence.Models
{
    public class ModuleGroups
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Semester")]
        public int SemesterID { get; set; }

        [ForeignKey("Module")]
        public int ModuleID { get; set; }

        [ForeignKey("Group")]
        public string GroupID { get; set; }

        public virtual Semester Semester { get; set; }
        public virtual Group Group { get; set; }
        public virtual Module Module { get; set; }
    }
}