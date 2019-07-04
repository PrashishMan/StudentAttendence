using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentAttendence.Models
{
    public class FacultyGroup
    {
        public string FacultyName {get; set; }
        public string GroupID { get; set; }
        public DateTime CreateDate { get; set; }
        public int FacultyID { get; set; }

        public FacultyGroup()
        {
        }

        public FacultyGroup(string groupID, DateTime createDate, int facultyID, string facultyName)
        {
            FacultyName = facultyName;
            GroupID = groupID;
            CreateDate = createDate;
            FacultyID = facultyID;
        }
    }
}