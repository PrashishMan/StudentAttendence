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
        


        public void CreateFaculty(Faculty faculty) {
            string createQuery = "INSERT INTO faculties (FacultyName, Status) VALUES('" + faculty.FacultyName + "', 1)";
            ExecuteQuery(createQuery);
        }
        public List<Faculty> GetFaculty()
        {

            string retriveListString = "SELECT * FROM Faculties WHERE STATUS = 1";

            SqlCommand cmd = new SqlCommand(retriveListString, con);
            List<Faculty> facultyList = new List<Faculty>();
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            int FacutlyID = reader.GetInt32(0);
                            string FacultyName = reader.GetString(1);
                            Faculty faculty = new Faculty(FacutlyID, FacultyName);
                            facultyList.Add(faculty);
                        }
                        reader.NextResult();
                    }
                }
                return facultyList;
            }
            finally
            {
                con.Close();

            }

        }

        public Faculty GetFaculty(int id)
        {
            string retriveString = "SELECT * from Faculties WHERE facultyID = " + id + " AND STATUS = 1 ";

            SqlCommand cmd = new SqlCommand(retriveString, con);
            Faculty faculty = new Faculty();
            try
            {
                con.Open();
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        faculty.FacultyID = int.Parse(oReader["FacultyID"].ToString());
                        faculty.FacultyName = oReader["FacultyName"].ToString();
                    }

                    con.Close();
                }
                return faculty;
            }
            finally
            {
                con.Close();

            }
        }

        public void UpdateFaculty(Faculty faculty)
        {
            string updateQuery = "UPDATE faculties SET FacultyName = '" + faculty.FacultyName + "' WHERE FacultyID = " + faculty.FacultyID + " ;";
            ExecuteQuery(updateQuery);
        }


        public void DeleteFaculty(int id)
        {
            string deleteQuery = "Delete FROM Faculties where facultyID = " + id + " ;";
            ExecuteQuery(deleteQuery);            
        }
    }
}