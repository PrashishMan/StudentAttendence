using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentAttendence.Models
{
    public class GroupModulesSemester
    {
        public int ID;
        public int ModuleID;
        public int SemesterID;
        public string GroupID;
        public string ModuleName;
        public int SemesterNo;

        public GroupModulesSemester()
        {
        }

        public GroupModulesSemester(int moduleID, int semesterID, string groupID, string moduleName, int semesterNo)
        {
            ModuleID = moduleID;
            SemesterID = semesterID;
            GroupID = groupID;
            ModuleName = moduleName;
            SemesterNo = semesterNo;
        }

        public GroupModulesSemester(int iD, int moduleID, int semesterID, string groupID, string moduleName, int semesterNo)
        {
            ID = iD;
            ModuleID = moduleID;
            SemesterID = semesterID;
            GroupID = groupID;
            ModuleName = moduleName;
            SemesterNo = semesterNo;
        }

        
    }
}