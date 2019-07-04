using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentAttendence.Models
{
    public class TeacherTeachingHrs
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public int TotalMinute { get; set; }
        public string ModuleName { get; set; }
        public string RoleName { get; set; }

        public TeacherTeachingHrs(string firstName, string lastName, string email, string contact, int totalMinute, string moduleName, string roleName)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Contact = contact;
            TotalMinute = totalMinute;
            ModuleName = moduleName;
            RoleName = roleName;
        }
    }
}