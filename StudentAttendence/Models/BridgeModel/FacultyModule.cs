using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentAttendence.Models
{
    public class FacultyModule
    {
        public int SemesterNo { get; set; }
        public int ModuleID { get; set; }

        public string ModuleName { get; set; }

        public string ModuleType { get; set; }

        public int Credit { get; set; }

        public int FacultyID { get; set; }

        public string FacultyName { get; set; }


        public FacultyModule(int ModuleID, string moduleName, string moduleType, int credit, int facultyID, string FacultyName)
        {
            ModuleID = ModuleID;
            ModuleName = moduleName;
            ModuleType = moduleType;
            Credit = credit;
            FacultyID = facultyID;
            FacultyName = FacultyName;
        }

        public FacultyModule(int ModuleID, int SemesterID, string moduleName, string moduleType, int credit, int facultyID, string FacultyName)
        {
            this.ModuleID = ModuleID;
            SemesterNo = SemesterID;
            ModuleName = moduleName;
            ModuleType = moduleType;
            Credit = credit;
            FacultyID = facultyID;
            this.FacultyName = FacultyName;
        }

        public FacultyModule(string moduleName, string moduleType, int credit, int facultyID,string FacultyName)
        {
            ModuleName = moduleName;
            ModuleType = moduleType;
            Credit = credit;
            FacultyID = facultyID;
            this.FacultyName = FacultyName;
        }

        public FacultyModule()
        {
         
        }

        
    }
}