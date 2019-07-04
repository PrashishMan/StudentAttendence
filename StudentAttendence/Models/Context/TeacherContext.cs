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

        public void CreateTeacher(Teacher teacher)
        {
            string createQuery = "INSERT INTO Teachers (FirstName, LastName, Email, Contact, HireDate, Status)" +
                "VALUES('" + teacher.FirstName + "','" + teacher.LastName + "','" + teacher.Email + "','" + teacher.Contact + "','" + teacher.HireDate + "', 1)";
            ExecuteQuery(createQuery);
        }

        public Teacher ReadTeacher(SqlDataReader reader)
        {
            Teacher teacher = new Teacher();
            while (reader.Read())
            {
                teacher.TeacherID = reader.GetInt32(0);
                teacher.FirstName = reader.GetString(1);
                teacher.LastName = reader.GetString(2);
                teacher.Email = reader.GetString(3);
                teacher.Contact = reader.GetString(4);
                teacher.HireDate = reader.GetDateTime(5);

            }
            return teacher;
        }


        public List<Teacher> GetTeacher()
        {
            string retriveStudentList = "SELECT TeacherID, FirstName, LastName, Email, Contact, HireDate FROM Teachers WHERE Status = 1 ;";
            List<Teacher> studentList = new List<Teacher>();
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
                            Teacher teacher = new Teacher();
                            teacher.TeacherID = reader.GetInt32(0);
                            teacher.FirstName = reader.GetString(1);
                            teacher.LastName = reader.GetString(2);
                            teacher.Email = reader.GetString(3);
                            teacher.Contact = reader.GetString(4);
                            teacher.HireDate = reader.GetDateTime(5);
                            studentList.Add(teacher);
                        }
                        reader.NextResult();
                    }
                    con.Close();
                }
                return studentList;
            }
            finally
            {
                con.Close();

            }

        }


        public Teacher GetTeacher(int teacherId)
        {
            string retriveString = "SELECT TeacherID, FirstName, LastName, Email, Contact, HireDate from Teachers WHERE TeacherID = '" + teacherId + "' ;";

            SqlCommand cmd = new SqlCommand(retriveString, con);
            Teacher teacher = new Teacher();
            try
            {
                con.Open();
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    teacher = this.ReadTeacher(oReader);
                    con.Close();
                }
                return teacher;
            }
            finally
            {
                con.Close();

            }
        }


        public void UpdateTeacher(Teacher teacher)
        {
            string updateQuery = "UPDATE Teachers " +
                "SET FirstName = '" + teacher.FirstName + "', LastName = '" + teacher.LastName + "', Email = '" + teacher.Email + "', Contact = '" + teacher.Contact + "', HireDate = '" + teacher.HireDate + "' WHERE TeacherID = '" + teacher.TeacherID + "' ;";
            ExecuteQuery(updateQuery);
        }


        public void DeleteTeacher(int id)
        {
            string deleteQuery = "UPDATE Teachers SET STATUS = 0 where TeacherID = '" + id + "' ;";
            ExecuteQuery(deleteQuery);
        }
    }
}