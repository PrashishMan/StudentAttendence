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

        public void CreateSemester(Semester semester)
        {
            string createQuery = "INSERT INTO Semesters (SemesterStartDate, SemesterEndDate, SemesterNo, Status)" +
                "VALUES('" + semester.SemesterStartDate + "','" + semester.SemesterEndDate + "','" + semester.SemesterNo + "', 1)";
            ExecuteQuery(createQuery);
        }

        public Semester ReadSemester(SqlDataReader reader)
        {
            Semester semester = new Semester();
            while (reader.Read())
            {
                semester.SemesterID = reader.GetInt32(0);
                semester.SemesterStartDate = reader.GetDateTime(1);
                semester.SemesterEndDate = reader.GetDateTime(2);
                semester.SemesterNo = reader.GetInt32(3);
            }
            return semester;
        }

        public List<Semester> GetSemester()
        {
            string retriveStudentList = "SELECT SemesterID, SemesterStartDate, SemesterEndDate, SemesterNo  FROM Semesters WHERE Status = 1 ;";
            List<Semester> semesterList = new List<Semester>();
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
                            Semester semester = new Semester();
                            semester.SemesterID = reader.GetInt32(0);
                            semester.SemesterStartDate = reader.GetDateTime(1);
                            semester.SemesterEndDate = reader.GetDateTime(2);
                            semester.SemesterNo = reader.GetInt32(3);
                            semesterList.Add(semester);
                        }
                        reader.NextResult();
                    }
                    con.Close();
                }
                return semesterList;
            }
            finally
            {
                con.Close();

            }

        }


        public Semester GetSemester(int semesterID)
        {
            string retriveString = "SELECT SemesterID, SemesterStartDate, SemesterEndDate, SemesterNo from Semesters WHERE SemesterID = " + semesterID + " ;";

            SqlCommand cmd = new SqlCommand(retriveString, con);
            Semester semester = new Semester();
            try
            {
                con.Open();
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    semester = this.ReadSemester(oReader);
                    con.Close();
                }
                return semester;
            }
            finally
            {
                con.Close();

            }
        }

        public void UpdateSemeseter(Semester semester)
        {
            string updateQuery = "UPDATE Semesters " +
                "SET SemesterStartDate = '" + semester.SemesterStartDate + "', SemesterEndDate = '" + semester.SemesterEndDate + "', SemesterNo = '" + semester.SemesterNo + "' WHERE SemesterID = " + semester.SemesterID + " ;";
            ExecuteQuery(updateQuery);
        }


        public void DeleteSemester(int id)
        {
            string deleteQuery = "UPDATE Semesters SET Status = 0 where SemesterID = " + id + " ;";
            ExecuteQuery(deleteQuery);
        }

        
    }
}