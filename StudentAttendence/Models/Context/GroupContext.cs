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
        public void CreateGroup(Group group)
        {
            string createQuery = "INSERT INTO groups (GroupID, CreateDate, FacultyID, Status) " +
                "VALUES('" + group.GroupID + "', '" + group.CreateDate + "','" + group.FacultyID + "', 1)";
            ExecuteQuery(createQuery);
        }

        public Group ReadGroup(SqlDataReader reader) {
            Group group = new Group();
            while (reader.Read())
            {
                group.GroupID = reader.GetString(0);
                group.CreateDate = reader.GetDateTime(1);
                group.FacultyID = reader.GetInt32(2);
            }
            return group;
        }

        public FacultyGroup ReadFacultyGroup(SqlDataReader oReader){
            FacultyGroup facultyGroup = new FacultyGroup();
            while (oReader.Read())
            {
                facultyGroup.GroupID = oReader.GetString(0);
                facultyGroup.CreateDate = oReader.GetDateTime(1);
                facultyGroup.FacultyName = oReader.GetString(2);
                facultyGroup.FacultyID = oReader.GetInt32(3);
            }
            return facultyGroup;
        }

        public List<Group> GetGroup() {
            string retriveGroupList = "SELECT GroupID, CreateDate, FacultyID FROM GROUPS ";
            List<Group> groupList = new List<Group>();
            SqlCommand cmd = new SqlCommand(retriveGroupList, con);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            string GroupID = reader.GetString(0);
                            DateTime CreateDate = reader.GetDateTime(1);
                            int FacultyID = reader.GetInt32(2);
                            Group group = new Group(GroupID, CreateDate, FacultyID);
                            groupList.Add(group);
                        }
                        reader.NextResult();
                    }
                    con.Close();
                }
                return groupList;
            }
            finally
            {
                con.Close();

            }

        }

        public List<FacultyGroup> GetFacultyGroup(){
            string retriveGroupList = "SELECT g.GroupID, g.CreateDate, f.FacultyID, f.FacultyName FROM GROUPS g JOIN Faculties f ON" +
                " g.FacultyID = f.FacultyID AND g.Status = 1";
            List<FacultyGroup> facultyGroupList = new List<FacultyGroup>();
            SqlCommand cmd = new SqlCommand(retriveGroupList, con);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            string GroupID = reader.GetString(0);
                            DateTime CreateDate = reader.GetDateTime(1);
                            int FacultyID = reader.GetInt32(2);
                            string FacultyName = reader.GetString(3);

                            FacultyGroup facultyGroup = new FacultyGroup(GroupID, CreateDate, FacultyID, FacultyName);
                            facultyGroupList.Add(facultyGroup);
                        }
                        reader.NextResult();
                    }
                    con.Close();
                }
                return facultyGroupList;
            }
            finally
            {
                con.Close();

            }

        }


        public Group GetGroup(string id){
            string retriveString = "SELECT GroupID, CreateDate, FacultyID from Groups WHERE GroupID = '" + id + "' ;";

            SqlCommand cmd = new SqlCommand(retriveString, con);
            Group group = new Group();
            try
            {
                con.Open();
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    group = this.ReadGroup(oReader);
                    con.Close();
                }
                return group;
            }
            finally
            {
                con.Close();

            }
        }


        public FacultyGroup GetFacultyGroup(string id){
            string retriveString = "SELECT g.GroupID as \"GroupID\", g.CreateDate as \"CreateDate\", f.FacultyName as \"FacultyName\", f.FacultyID as \"FacultyID\" from Groups g JOIN Faculties f ON f.FacultyID = g.FacultyID AND g.GroupID = '" + id + "' ;";

            SqlCommand cmd = new SqlCommand(retriveString, con);
            FacultyGroup facultyGroup = new FacultyGroup();
            try
            {
                con.Open();
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    facultyGroup=this.ReadFacultyGroup(oReader);
                    con.Close();
                }
                return facultyGroup;
            }
            finally
            {
                con.Close();

            }
        }



        public void UpdateGroup(Group group)
        {
            string updateQuery = "UPDATE Groups " +
                "SET GroupID = '" + group.GroupID + "', CreateDate = '" + group.CreateDate + "', FacultyID = '" + group.FacultyID + "' WHERE GroupID = '" + group.GroupID + "' ;";
            ExecuteQuery(updateQuery);
        }


        public void DeleteGroup(string id)
        {
            string delete = "DELETE FROM Groups WHERE groupID = '" + id + "' ;";
            string deleteQuery = "UPDATE Groups SET STATUS = 0 WHERE groupID = '" + id + "' ;";
            ExecuteQuery(delete);
        }
    }
}