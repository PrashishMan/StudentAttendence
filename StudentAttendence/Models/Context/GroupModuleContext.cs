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

        public void CreateGroupModule(ModuleGroups groupModules)
        {
            string createQuery = "INSERT INTO ModuleGroups (GroupID, SemesterID, ModuleID)" +
                "VALUES('" + groupModules.GroupID + "', '" + groupModules.SemesterID + "', '"+ groupModules.ModuleID + "') ;";
            ExecuteQuery(createQuery);
        }

        public ModuleGroups ReadGroupModule(SqlDataReader reader)
        {
            ModuleGroups groupModule = new ModuleGroups();
            while (reader.Read())
            {
                groupModule.ID = reader.GetInt32(0);
                groupModule.GroupID = reader.GetString(1);
                groupModule.SemesterID = reader.GetInt32(2);
                groupModule.ModuleID = reader.GetInt32(3);
                
            }
            return groupModule;
        }

        public GroupModulesSemester ReadGroupSemesterModule(SqlDataReader reader)
        {
            GroupModulesSemester groupModule = new GroupModulesSemester();
            while (reader.Read())
            {
                groupModule.ID = reader.GetInt32(0);
                groupModule.GroupID = reader.GetString(1);
                groupModule.SemesterID = reader.GetInt32(2);
                groupModule.ModuleID = reader.GetInt32(3);
                groupModule.ModuleName = reader.GetString(4);
                groupModule.SemesterNo = reader.GetInt32(5);

            }
            return groupModule;
        }


        public List<GroupModulesSemester> GetGroupSemesterModules()
        {
            string retriveStudentList = "SELECT g.ID, g.GroupID, g.SemesterID, g.ModuleID, m.ModuleName, s.SemesterNo FROM ModuleGroups g " +
                "Join Semesters s ON s.SemesterID = g.SemesterID " +
                "Join Modules m ON m.ModuleID = g.ModuleID ;"; 
            List<GroupModulesSemester> groupModuleSemesterList = new List<GroupModulesSemester>();
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
                            GroupModulesSemester groupModule = new GroupModulesSemester();
                            groupModule.ID = reader.GetInt32(0);
                            groupModule.GroupID = reader.GetString(1);
                            groupModule.SemesterID = reader.GetInt32(2);
                            groupModule.ModuleID = reader.GetInt32(3);
                            groupModule.ModuleName = reader.GetString(4);
                            groupModule.SemesterNo = reader.GetInt32(5);
                            groupModuleSemesterList.Add(groupModule);
                        }
                        reader.NextResult();
                    }
                    con.Close();
                }
                return groupModuleSemesterList;
            }
            finally
            {
                con.Close();

            }

        }

        //public List<GroupTimetableBridge> GetGroupModulesBridge()
        //{
        //    string retriveStudentList = "SELECT g.GroupID, m.ModuleName, t.ClassStartTime, t.ClassEndTime, t.Day FROM GroupTimetables gt " +
        //        "Join Groups g ON g.GroupID = gt.GroupID " +
        //        "Join Timetables t ON t.TimetableID = gt.TimetableID ;";
        //    List<ModuleTimetableBridge> moduleTimetables = new List<ModuleTimetableBridge>();
        //    SqlCommand cmd = new SqlCommand(retriveStudentList, con);
        //    try
        //    {
        //        con.Open();
        //        using (SqlDataReader reader = cmd.ExecuteReader())
        //        {
        //            while (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    ModuleTimetableBridge moduleTimetable = new ModuleTimetableBridge();
        //                    moduleTimetable.GroupID = reader.GetInt32(0);
        //                    moduleTimetable.GroupID = reader.GetString(1);
        //                    moduleTimetable.SemesterID = reader.GetInt32(2);
        //                    moduleTimetable.ModuleID = reader.GetInt32(3);
        //                    moduleTimetable.ModuleName = reader.GetString(4);
        //                    groupModule.SemesterNo = reader.GetInt32(5);
        //                    groupModuleSemesterList.Add(groupModule);
        //                }
        //                reader.NextResult();
        //            }
        //            con.Close();
        //        }
        //        return groupModuleSemesterList;
        //    }
        //    finally
        //    {
        //        con.Close();

        //    }

        //}

        public GroupModulesSemester GetGroupModulesSemester(int id)
        {
            string retriveGroupModuleList = "SELECT g.ID, g.GroupID, g.SemesterID, g.ModuleID, m.ModuleName, s.SemesterNo FROM ModuleGroups g " +
                "Join Semesters s ON s.SemesterID = g.SemesterID AND g.ID = " +id+
                " Join Modules m ON m.ModuleID = g.ModuleID ;";

            SqlCommand cmd = new SqlCommand(retriveGroupModuleList, con);
            GroupModulesSemester groupModule = new GroupModulesSemester();
            try
            {
                con.Open();
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    groupModule = this.ReadGroupSemesterModule(oReader);
                    con.Close();
                }
                return groupModule;
            }
            finally
            {
                con.Close();

            }
        }

        public List<ModuleGroups> GetGroupModules()
        {
            string retriveStudentList = "SELECT ID, GroupID, SemesterID, ModuleID FROM ModuleGroups ;";
            List<ModuleGroups> groupModuleList = new List<ModuleGroups>();
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
                            ModuleGroups groupModule = new ModuleGroups();
                            groupModule.ID = reader.GetInt32(0);
                            groupModule.GroupID = reader.GetString(1);
                            groupModule.SemesterID = reader.GetInt32(2);
                            groupModule.ModuleID = reader.GetInt32(3);
                            groupModuleList.Add(groupModule);
                        }
                        reader.NextResult();
                    }
                    con.Close();
                }
                return groupModuleList;
            }
            finally
            {
                con.Close();

            }

        }


        public ModuleGroups GetGroupModule(int id)
        {
            string retriveString = "SELECT ID, GroupID, SemesterID, ModuleID from ModuleGroups WHERE ID = " + id + " ;";

            SqlCommand cmd = new SqlCommand(retriveString, con);
            ModuleGroups groupModule = new ModuleGroups();
            try
            {
                con.Open();
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    groupModule = this.ReadGroupModule(oReader);
                    con.Close();
                }
                return groupModule;
            }
            finally
            {
                con.Close();

            }
        }

        public void UpdateGroupModule(ModuleGroups moduleGroups)
        {
            string updateQuery = "UPDATE ModuleGroups SET SemesterID = " +moduleGroups.SemesterID+ ", ModuleID= " + moduleGroups.ModuleID +  ", GroupID = '" + moduleGroups.GroupID + "' WHERE ID = " + moduleGroups.ID+" ; ";
            ExecuteQuery(updateQuery);
        }

        public void DeletGroupModule(int id)
        {
            string deleteQuery = "DELETE FROM ModuleGroups WHERE ID = " + id + " ;";
            ExecuteQuery(deleteQuery);
        }
    }
}