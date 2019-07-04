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

        public void CreateStudent(Student student)
        {
            string createQuery = "INSERT INTO Students (FirstName, LastName, Email, Contact,EnrolledDate ,GroupID, Status)" +
                "VALUES('" + student.FirstName + "','" + student.LastName + "','" + student.Email + "','" + student.Contact + "','" + student.EnrolledDate + "','" + student.GroupID + "', 1)";
            ExecuteQuery(createQuery);
        }

        public Student ReadStudent(SqlDataReader reader)
        {
            Student student = new Student();
            while (reader.Read())
            {
                student.StudentID = reader.GetInt32(0);
                student.FirstName = reader.GetString(1);
                student.LastName = reader.GetString(2);
                student.Email = reader.GetString(3);
                student.Contact = reader.GetString(4);
                student.EnrolledDate = reader.GetDateTime(5);
                student.GroupID = reader.GetString(6);
                
            }
            return student;
        }

        public List<Student> GetGroupStudent(string GroupID)
        {
            string retriveStudentList = "SELECT s.StudentID, s.FirstName, s.LastName, s.Email, s.Contact,s.EnrolledDate, s.GroupID FROM Students s " +
                "JOIN  Groups g ON g.GroupID = s.GroupID AND g.GroupID = '"+ GroupID + "' AND s.Status = 1 ORDER BY s.EnrolledDate DESC;";
            List<Student> studentList = new List<Student>();
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
                            Student student = new Student();
                            student.StudentID = reader.GetInt32(0);
                            student.FirstName = reader.GetString(1);
                            student.LastName = reader.GetString(2);
                            student.Email = reader.GetString(3);
                            student.Contact = reader.GetString(4);
                            student.EnrolledDate = reader.GetDateTime(5);
                            student.GroupID = reader.GetString(6);
                            studentList.Add(student);
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

        public List<Student> GetStudent()
        {
            string retriveStudentList = "SELECT s.StudentID, s.FirstName, s.LastName, s.Email, s.Contact,s.EnrolledDate, s.GroupID FROM Students s WHERE s.Status = 1 ORDER BY s.FirstName DESC;";
            List<Student> studentList = new List<Student>();
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
                            Student student = new Student();
                            student.StudentID = reader.GetInt32(0);
                            student.FirstName = reader.GetString(1);
                            student.LastName = reader.GetString(2);
                            student.Email = reader.GetString(3);
                            student.Contact = reader.GetString(4);
                            student.EnrolledDate = reader.GetDateTime(5);
                            student.GroupID = reader.GetString(6);
                            studentList.Add(student);
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

        public List<Student> GetStudentByEnrolldate()
        {
            string retriveStudentList = "SELECT s.StudentID, s.FirstName, s.LastName, s.Email, s.Contact,s.EnrolledDate, s.GroupID FROM Students s WHERE s.Status = 1 ORDER BY s.enrolledDate DESC;";
            List<Student> studentList = new List<Student>();
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
                            Student student = new Student();
                            student.StudentID = reader.GetInt32(0);
                            student.FirstName = reader.GetString(1);
                            student.LastName = reader.GetString(2);
                            student.Email = reader.GetString(3);
                            student.Contact = reader.GetString(4);
                            student.EnrolledDate = reader.GetDateTime(5);
                            student.GroupID = reader.GetString(6);
                            studentList.Add(student);
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


        public Student GetStudent(int studentId)
        {
            string retriveString = "SELECT StudentID, FirstName, LastName, Email, Contact, EnrolledDate, GroupID from Students WHERE StudentID = '" + studentId + "' ;";

            SqlCommand cmd = new SqlCommand(retriveString, con);
            Student student = new Student();
            try
            {
                con.Open();
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    student = this.ReadStudent(oReader);
                    con.Close();
                }
                return student;
            }
            finally
            {
                con.Close();

            }
        }


        public void UpdateStudent(Student student)
        {
            string updateQuery = "UPDATE Students " +
                "SET FirstName = '" + student.FirstName + "', LastName = '" + student.LastName + "', Email = '" + student.Email + "', Contact = '" + student.Contact + "', EnrolledDate = '" + student.EnrolledDate + "', GroupID = '" + student.GroupID + "' WHERE StudentID = '" + student.StudentID + "' ;";
            ExecuteQuery(updateQuery);
        }


        public void DeleteStudent(int id)
        {
            string deleteQuery = "UPDATE Students SET STATUS = 0 where StudentID = '" + id + "' ;";
            ExecuteQuery(deleteQuery);
        }


    }
}