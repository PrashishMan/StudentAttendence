using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentAttendence.Models
{
    public class TeacherRoleModule
    {
        public string FirstName  { get; set; }

        public string LastName { get; set; }

        public string ModuleName { get; set; }

        public string RoleName { get; set; }

        public int ID { get; set; }

        public TeacherRoleModule()
        {
        }

        public TeacherRoleModule(string firstName, string lastName, string moduleName, string roleName)
        {
            FirstName = firstName;
            LastName = lastName;
            ModuleName = moduleName;
            RoleName = roleName;
        }

        public TeacherRoleModule(string firstName, string moduleName, string roleName, int iD)
        {
            FirstName = firstName;
            ModuleName = moduleName;
            RoleName = roleName;
            ID = iD;
        }
    }
}