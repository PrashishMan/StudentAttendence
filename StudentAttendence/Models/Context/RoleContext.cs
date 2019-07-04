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

        public void CreateRole(Role role)
        {
            string createQuery = "INSERT INTO Roles (RoleName, Qualification, Status)" +
                "VALUES('" + role.RoleName + "','" + role.Qualification + "', 1)";
            ExecuteQuery(createQuery);
        }

        public Role ReadRole(SqlDataReader reader)
        {
            Role role = new Role();
            while (reader.Read())
            {
                role.RoleID = reader.GetInt32(0);
                role.RoleName = reader.GetString(1);
                role.Qualification = reader.GetString(2);
                role.Status = true;
            }
            return role;
        }

        public List<Role> GetRole()
        {
            string retriveStudentList = "SELECT RoleID, RoleName, Qualification FROM Roles WHERE Status = 1 ;";
            List<Role> roleList = new List<Role>();
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
                            Role role = new Role();
                            role.RoleID = reader.GetInt32(0);
                            role.RoleName = reader.GetString(1);
                            role.Qualification = reader.GetString(2);
                            roleList.Add(role);
                        }
                        reader.NextResult();
                    }
                    con.Close();
                }
                return roleList;
            }
            finally
            {
                con.Close();

            }

        }


        public Role GetRole(int roleID)
        {
            string retriveString = "SELECT RoleID, RoleName, Qualification from Roles WHERE RoleID = " + roleID + " ;";

            SqlCommand cmd = new SqlCommand(retriveString, con);
            Role role = new Role();
            try
            {
                con.Open();
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    role = this.ReadRole(oReader);
                    con.Close();
                }
                return role;
            }
            finally
            {
                con.Close();

            }
        }

        public void UpdateRole(Role role)
        {
            string updateQuery = "UPDATE Roles " +
                "SET RoleName = '" + role.RoleName + "', Qualification = '" + role.Qualification + "' WHERE RoleID = " + role.RoleID +" ;";
            ExecuteQuery(updateQuery);
        }


        public void DeleteRole(int id)
        {
            string deleteQuery = "UPDATE Roles SET STATUS = 0 where RoleID = " + id + " ;";
            ExecuteQuery(deleteQuery);
        }
    }
}