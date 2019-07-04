using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentAttendence.Models
{
    public class GroupTimetableBridge
    {
        public int ID { get; set; }
        public string GroupID { get; set; }
        public string ModuleName { get; set; }
        public TimeSpan ClassStartTime { get; set; }
        public TimeSpan ClassEndTime { get; set; }
        public string Day { get; set; }

        public GroupTimetableBridge(int ID, string groupID, string moduleName, TimeSpan classStartTime, TimeSpan classEndTime, string day)
        {
            this.ID = ID;
            GroupID = groupID;
            ModuleName = moduleName;
            ClassStartTime = classStartTime;
            ClassEndTime = classEndTime;
            Day = day;
        }

        public GroupTimetableBridge()
        {
        }
    }
}