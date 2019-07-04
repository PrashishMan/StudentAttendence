using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StudentAttendence.Models
{
    partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public void CreateTeacherRole(TeacherRole teacherRole)
        {
            string createQuery = "INSERT INTO teacherRoles(TeacherID, ModuleID, RoleID) VALUES("+ teacherRole.TeacherID + "," + teacherRole.ModuleID + "," + teacherRole.RoleID +");";
            ExecuteQuery(createQuery);
        }

        public TeacherRoleModule ReadTeacherRole(SqlDataReader reader)
        {
            TeacherRoleModule teacherRole = new TeacherRoleModule();
            while (reader.Read())
            {
                teacherRole.ID = reader.GetInt32(0);
                teacherRole.FirstName = reader.GetString(1);
                teacherRole.ModuleName = reader.GetString(2);
                teacherRole.RoleName = reader.GetString(3);

            }
            return teacherRole;
        }


        public List<TeacherRoleModule> GetSemesterModule(int facultyid, int semesterid)
        {
            string retriveListString = "SELECT t.FirstName, t.LastName, r.RoleName, m.ModuleName, tr.ID FROM teacherRoles tr " +
                "JOIN Teachers t ON tr.TeacherID = t.TeacherID " +
                "JOIN Roles r ON tr.RoleID = r.RoleID " +
                "JOIN Modules m ON tr.ModuleID = m.ModuleID AND m.FacultyID = " + facultyid + 
                " AND m.semesterid = " + semesterid;

            SqlCommand cmd = new SqlCommand(retriveListString, con);
            try
            {
                List<TeacherRoleModule> teacherModulesList = new List<TeacherRoleModule>();
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            TeacherRoleModule teacherRole = new TeacherRoleModule();
                            teacherRole.FirstName = reader.GetString(0);
                            teacherRole.LastName = reader.GetString(1);
                            teacherRole.RoleName = reader.GetString(2);
                            teacherRole.ModuleName = reader.GetString(3);
                            teacherRole.ID = reader.GetInt32(4);
                            teacherModulesList.Add(teacherRole);
                        }
                        reader.NextResult();
                    }
                }
                return teacherModulesList;

            }
            finally
            {
                con.Close();
            }
        }

        public List<TeacherRoleModule> GetModuleTeachers(int moduleid)
        {
            string retriveListString = "SELECT t.FirstName, t.LastName, r.RoleName, m.ModuleName, tr.ID FROM teacherRoles tr " +
                "JOIN Teachers t ON tr.TeacherID = t.TeacherID " +
                "JOIN Roles r ON tr.RoleID = r.RoleID " +
                "JOIN Modules m ON tr.ModuleID = m.ModuleID AND m.ModuleID = " + moduleid;

            SqlCommand cmd = new SqlCommand(retriveListString, con);
            try
            {
                List<TeacherRoleModule> teacherModulesList = new List<TeacherRoleModule>();
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {   
                    while (reader.HasRows)
                    {
                        
                        while (reader.Read())
                        {
                            TeacherRoleModule teacherRole = new TeacherRoleModule();
                            teacherRole.FirstName = reader.GetString(0);
                            teacherRole.LastName = reader.GetString(1);
                            teacherRole.RoleName = reader.GetString(2);
                            teacherRole.ModuleName = reader.GetString(3);
                            teacherRole.ID = reader.GetInt32(4);
                            teacherModulesList.Add(teacherRole);
                        }
                        reader.NextResult();
                    }
                }
                return teacherModulesList;

            }
            finally
            {
                con.Close();
            }
        }


        public TeacherRoleModule GetTeacherRoleModule(int id)
        {
            string retriveListString = "SELECT t.FirstName, r.RoleName, m.ModuleName, tr.ID FROM teacherRoles tr " +
                "JOIN Teachers t ON tr.TeacherID = t.TeacherID " +
                "JOIN Roles r ON tr.RoleID = r.RoleID " +
                "JOIN Modules m ON tr.ModuleID = m.ModuleID " +
                "AND tr.ID = " + id
                ;

            SqlCommand cmd = new SqlCommand(retriveListString, con);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    TeacherRoleModule teacherRole = new TeacherRoleModule();

                    while (reader.Read())
                    {
                        
                        teacherRole.FirstName = reader.GetString(0);
                        teacherRole.RoleName = reader.GetString(1);
                        teacherRole.ModuleName = reader.GetString(2);
                        teacherRole.ID = reader.GetInt32(3);
                    }
                    return teacherRole;
                }
                
            }
            finally
            {
                con.Close();
            }
        }

        public List<TeacherRoleModule> GetTeacherRoleModule()
        {
            string retriveListString = "SELECT t.FirstName, r.RoleName, m.ModuleName, tr.ID FROM teacherRoles tr " +
                "JOIN Teachers t ON tr.TeacherID = t.TeacherID " +
                "JOIN Roles r ON tr.RoleID = r.RoleID " +
                "JOIN Modules m ON tr.ModuleID = m.ModuleID"
                ; 

            SqlCommand cmd = new SqlCommand(retriveListString, con);
            List<TeacherRoleModule> teacherRoleList = new List<TeacherRoleModule>();
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.HasRows)
                    {   
                        while (reader.Read())
                        {
                            TeacherRoleModule teacherRole = new TeacherRoleModule();
                            teacherRole.FirstName = reader.GetString(0);
                            teacherRole.RoleName = reader.GetString(1);
                            teacherRole.ModuleName = reader.GetString(2);
                            teacherRole.ID = reader.GetInt32(3);

                            teacherRoleList.Add(teacherRole);
                        }
                        reader.NextResult();
                    }
                }
                return teacherRoleList;
            }
            finally
            {
                con.Close();
            }
        }


        public List<TeacherRole> GetTeacherRole()
        {

            string retriveListString = "SELECT * FROM teacherRoles";

            SqlCommand cmd = new SqlCommand(retriveListString, con);
            List<TeacherRole> teacherRoleList = new List<TeacherRole>();
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            int TeacherID = reader.GetInt32(0);
                            int ModuleID = reader.GetInt32(1);
                            int RoleID = reader.GetInt32(2);
                            TeacherRole teacherRole = new TeacherRole(TeacherID, ModuleID, RoleID);
                            teacherRoleList.Add(teacherRole);
                        }
                        reader.NextResult();
                    }
                }
                return teacherRoleList;
            }
            finally
            {
                con.Close();

            }

        }

        public TeacherRole GetTeacherRole(int id)
        {
            string retriveString = "SELECT * from teacherRoles WHERE ID = " + id;

            SqlCommand cmd = new SqlCommand(retriveString, con);
            TeacherRole teacherRole = new TeacherRole();
            try
            {
                con.Open();
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        teacherRole.TeacherID = int.Parse(oReader["TeacherID"].ToString());
                        teacherRole.ModuleID = int.Parse(oReader["ModuleID"].ToString());
                        teacherRole.RoleID = int.Parse(oReader["RoleID"].ToString());

                    }

                    con.Close();
                }
                return teacherRole;
            }
            finally
            {
                con.Close();

            }
        }

        public void UpdateTeacherRole(TeacherRole teacherRole)
        {
            string updateQuery = "UPDATE teacherRoles SET TeacherID = '" + teacherRole.TeacherID + "," + teacherRole.ModuleID + "," + teacherRole.RoleID + "' WHERE ID = " + teacherRole.ID + " ;";
            ExecuteQuery(updateQuery);
        }


        public void DeleteTeacherRole(int id)
        {
            string deleteQuery = "Delete FROM teacherRoles where ID = " + id + " ;";
            ExecuteQuery(deleteQuery);
        }
    }
}