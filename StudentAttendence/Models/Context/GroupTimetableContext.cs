using Microsoft.AspNet.Identity.EntityFramework;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StudentAttendence.Models
{

    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public void CreateGroupTimetable(GroupTimetable groupTimetable)
        {
            string createQuery = "INSERT INTO GroupTimetables (GroupID, TimetableID)" +
                "VALUES('" + groupTimetable.GroupID + "','" + groupTimetable.TimetableID + "' )";
            ExecuteQuery(createQuery);
        }

        public GroupTimetable ReadGroupTimetable(SqlDataReader reader)
        {
            GroupTimetable groupTimetable = new GroupTimetable();
            while (reader.Read())
            {
                groupTimetable.ID = reader.GetInt32(0);
                groupTimetable.GroupID = reader.GetString(1);
                groupTimetable.TimetableID = reader.GetInt32(2);

            }
            return groupTimetable;
        }

        public ModuleTimetableBridge ReadGroupTimetableBridge(SqlDataReader reader)
        {
            ModuleTimetableBridge moduleTimetable = new ModuleTimetableBridge();
            while (reader.Read())
            {
                moduleTimetable.TimeTableId = reader.GetInt32(0);
                moduleTimetable.ClassStartTime = reader.GetTimeSpan(1);
                moduleTimetable.ClassEndTime = reader.GetTimeSpan(2);
                moduleTimetable.Day = reader.GetString(3);
                moduleTimetable.Room = reader.GetString(4);
                moduleTimetable.Year = reader.GetInt32(5);
                moduleTimetable.ModuleID = reader.GetInt32(6);
                moduleTimetable.ModuleName = reader.GetString(7);
                moduleTimetable.FacultyName = reader.GetString(8);
                moduleTimetable.Semester = reader.GetInt32(9);
            }
            return moduleTimetable;
        }

        public List<GroupTimetable> GetGroupTimetable()
        {
            string query = "SELECT ID, GroupID, TimetableID FROM GroupTimetables ;";
            List<GroupTimetable> groupTimetableList = new List<GroupTimetable>();
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.HasRows)
                    {

                        while (reader.Read())
                        {

                            int ID = reader.GetInt32(0);
                            string GroupID = reader.GetString(1);
                            int TimetableID = reader.GetInt32(2);

                            GroupTimetable groupTimetable = new GroupTimetable(ID, GroupID, TimetableID);
                            groupTimetableList.Add(groupTimetable);
                        }
                        reader.NextResult();
                    }
                    con.Close();
                }
                return groupTimetableList;
            }
            finally
            {
                con.Close();

            }

        }

        public GroupTimetableBridge GetGroupTimetableBridge(int id)
        {
            string retriveStudentList = "SELECT gt.ID, g.GroupID, m.ModuleName, t.ClassStartTime, t.ClassEndTime, t.Day " +
                                        "FROM GroupTimetables gt " +
                                        "Join Groups g ON g.GroupID = gt.GroupID AND gt.ID = " + id +
                                        " Join Timetables t ON t.TimetableID = gt.TimetableID " +
                                        "Join Modules m ON m.ModuleID = t.ModuleID ;";

            SqlCommand cmd = new SqlCommand(retriveStudentList, con);
            try
            {
                con.Open();
                GroupTimetableBridge groupTimetableBridge = new GroupTimetableBridge();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        {
                            groupTimetableBridge.ID = reader.GetInt32(0);
                            groupTimetableBridge.GroupID = reader.GetString(1);
                            groupTimetableBridge.ModuleName = reader.GetString(2);
                            groupTimetableBridge.ClassStartTime = reader.GetTimeSpan(3);
                            groupTimetableBridge.ClassEndTime = reader.GetTimeSpan(4);
                            groupTimetableBridge.Day = reader.GetString(5);
                        }
                    con.Close();
                }
                return groupTimetableBridge;
            }
            finally
            {
                con.Close();

            }

        }

        public List<GroupTimetableBridge> GetGroupTimetableBridge()
        {
            string retriveStudentList = "SELECT gt.ID, g.GroupID, m.ModuleName, t.ClassStartTime, t.ClassEndTime, t.Day " +
                                         "FROM GroupTimetables gt " +
                                        "Join Groups g ON g.GroupID = gt.GroupID " +
                                        "Join Timetables t ON t.TimetableID = gt.TimetableID " +
                                        "Join Modules m ON m.ModuleID = t.ModuleID ;";

            List <GroupTimetableBridge> groupTimetableList = new List<GroupTimetableBridge>();
            SqlCommand cmd = new SqlCommand(retriveStudentList, con);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            GroupTimetableBridge groupTimetableBridge = new GroupTimetableBridge();
                            groupTimetableBridge.ID = reader.GetInt32(0);
                            groupTimetableBridge.GroupID = reader.GetString(1);
                            groupTimetableBridge.ModuleName = reader.GetString(2);
                            groupTimetableBridge.ClassStartTime = reader.GetTimeSpan(3);
                            groupTimetableBridge.ClassEndTime = reader.GetTimeSpan(4);
                            groupTimetableBridge.Day = reader.GetString(5);
                            groupTimetableList.Add(groupTimetableBridge);
                        }
                        reader.NextResult();
                    }
                    con.Close();
                }
                return groupTimetableList;
            }
            finally
            {
                con.Close();

            }

        }

        public GroupTimetable GetGroupTimetable(int ID)
        {
            string retriveString = "SELECT ID, GroupID, TimetableID FROM GroupTimetables WHERE ID = "+ID +";";

            SqlCommand cmd = new SqlCommand(retriveString, con);
            GroupTimetable groupTimetable = new GroupTimetable();
            try
            {
                con.Open();
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    groupTimetable = this.ReadGroupTimetable(oReader);
                    con.Close();
                }
                return groupTimetable;
            }
            finally
            {
                con.Close();

            }
        }


        public void UpdateGroupTimetable(GroupTimetable groupTimetable)
        {
            string updateQuery = "UPDATE GroupTimetables " +
                "SET GroupID = '" + groupTimetable.GroupID + "', TimetableID = '" + groupTimetable.TimetableID + "' ;";
            ExecuteQuery(updateQuery);
        }


        public void DeleteGroupTimetable(int id)
        {
            string deleteQuery = "DELETE FROM GroupTimetables WHERE ID = '" + id + "' ;";
            ExecuteQuery(deleteQuery);
        }
    }
}