using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentAttendence.Models
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string RoleName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Qualification { get; set; }

        public virtual List<TeacherRole> TeacherRoles { get; set; }

        public bool Status { get; set; }

        public Role()
        {
            this.Status = true;
        }

        public Role(int roleID, string roleName, string qualification, List<TeacherRole> teacherRoles, bool status)
        {
            RoleID = roleID;
            RoleName = roleName;
            Qualification = qualification;
            TeacherRoles = teacherRoles;
            Status = status;
        }

        public Role(string roleName, string qualification)
        {
            RoleName = roleName;
            Qualification = qualification;
            Status = true;
        }
    }
}