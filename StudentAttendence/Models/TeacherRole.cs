using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentAttendence.Models
{
    public class TeacherRole
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Teacher")]
        public int TeacherID { get; set; }

        [ForeignKey("Module")]
        public int ModuleID { get; set; }

        [ForeignKey("Role")]
        public int RoleID { get; set; }

        public virtual Teacher Teacher { get; set; }
        public virtual Module Module { get; set; }
        public virtual Role Role { get; set; }

        public TeacherRole()
        {

        }

        public TeacherRole(int TeacherID, int ModuleID, int RoleID)
        {
            this.TeacherID = TeacherID;
            this.ModuleID = ModuleID;
            this.RoleID = RoleID;
        }

        public TeacherRole(int id, int TeacherID, int ModuleID, int RoleID)
        {
            this.ID = ID;
            this.TeacherID = TeacherID;
            this.ModuleID = ModuleID;
            this.RoleID = RoleID;
        }
    }
}