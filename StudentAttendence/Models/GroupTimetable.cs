using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentAttendence.Models
{
    public class GroupTimetable
    {
        [Key]
        public int ID { get; set; }
        
        [ForeignKey("Group")]
        public string GroupID { get; set; }

        [ForeignKey("Timetable")]
        public int TimetableID { get; set; }

        public virtual Group Group { get; set; }
        public virtual Timetable Timetable { get; set; }

        public GroupTimetable()
        {
        }

        public GroupTimetable(int iD, string groupID, int timetableID)
        {
            ID = iD;
            GroupID = groupID;
            TimetableID = timetableID;
        }
    }
}