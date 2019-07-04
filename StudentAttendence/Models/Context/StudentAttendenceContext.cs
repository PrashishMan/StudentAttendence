using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentAttendence.Models
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public void CreateAttendence(Attendence attendence )
        {
                string createQuery = "INSERT INTO Attendences (StudentID, TimetableID, Date, Condition)" +
                "VALUES('" + attendence.StudentID + "','" + attendence.TimetableID + "','" + attendence.Date.Date + "','" + attendence.Condition + "' )";
                ExecuteQuery(createQuery);
        }

        public Attendence ReadAttendence(SqlDataReader reader)
        {
            Attendence attendence = new Attendence();
            while (reader.Read())
            {
                attendence.AttendenceID = reader.GetInt32(0);
                attendence.StudentID = reader.GetInt32(1);
                attendence.TimetableID = reader.GetInt32(2);
                attendence.Date = reader.GetDateTime(3).Date;
                attendence.Condition = reader.GetString(4);
            }
            return attendence;
        }

        public StudentsAttendence ReadStudentAttendence(SqlDataReader reader)
        {
            StudentsAttendence studentAttendence = new StudentsAttendence();
            while (reader.Read())
            {
                studentAttendence.ID = reader.GetInt32(0);
                studentAttendence.TimetableID = reader.GetInt32(1);
                studentAttendence.StudentID = reader.GetInt32(2);
                studentAttendence.FirstName = reader.GetString(3);
                studentAttendence.LastName = reader.GetString(4);
                studentAttendence.ClassStartTime = reader.GetTimeSpan(5);
                studentAttendence.ClassEndTime = reader.GetTimeSpan(6);
                studentAttendence.ModuleName = reader.GetString(7);
                studentAttendence.Date = reader.GetDateTime(8).Date;
                studentAttendence.Condition = reader.GetString(9);
            }
            return studentAttendence;
        }

       

        public List<SelectListItem> GetTouristStudent()
        {
            string retriveAbcentList = "SELECT CONCAT(s.FirstName,' ',s.LastName) as Name " +
                "FROM Attendences a " +
                "JOIN Students s On s.StudentID = a.StudentID AND a.condition NOT IN ('Present', 'Late') " +
                "GROUP BY s.FirstName, s.LastName";

            try
            {
                List<SelectListItem> studentNames = new List<SelectListItem>();
                SqlCommand cmd = new SqlCommand(retriveAbcentList, con);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string StudentName = reader.GetString(0);
                            studentNames.Add(
                                new SelectListItem
                                {
                                    Value = StudentName,
                                    Text = StudentName
                                }
                                );
                        }
                        reader.NextResult();
                    }
                    con.Close();
                    return studentNames;

                }
            }finally
            {
                con.Close();
            }
        }

        public List<StudentsAttendence> GetDailyAttendence(DateTime date) {
            string retriveTimetableList = "SELECT a.AttendenceID, t.TimeTableID, s.StudentID, s.FirstName, s.LastName,  t.ClassStartTime, t.ClassEndTime, m.ModuleName, a.Date, a.Condition " +
                "FROM Attendences a " +
                "JOIN Students s On s.StudentID = a.StudentID AND a.Date = '" + date + 
                "' JOIN Timetables t On t.TimetableID = a.TimetableID " +
                "JOIN Modules m ON m.ModuleID = t.ModuleID ;" ;
                
            List<StudentsAttendence> studentAttendenceList;
            SqlCommand cmd = new SqlCommand(retriveTimetableList, con);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    studentAttendenceList = this.getStudentAttendenceList(reader);
                }
                return studentAttendenceList;
            }
            finally
            {
                con.Close();

            }

        }

        public List<StudentsAttendence> GetMonthlyAttendence(DateTime date)
        {

            string startDateQuery = "SELECT DATEADD(MONTH, DATEDIFF(MONTH, 0, '" + "2019/4/25" + "'), 0)";
            string endDateQuery = "SELECT DATEADD(SECOND, -1, DATEADD(MONTH, 1,  DATEADD(MONTH, DATEDIFF(MONTH, 0,'" + "2019/4/25" + "') , 0) ))";
            string retriveTimetableList = "SELECT a.AttendenceID, t.TimeTableID, s.StudentID, s.FirstName, s.LastName,  t.ClassStartTime, t.ClassEndTime, m.ModuleName, a.Date, a.Condition " +
                "FROM Attendences a " +
                "JOIN Students s On s.StudentID = a.StudentID " +
                "JOIN Timetables t On t.TimetableID = a.TimetableID " +
                "JOIN Modules m ON m.ModuleID = t.ModuleID " +
                "WHERE a.Date BETWEEN ("+ startDateQuery + ") AND ("+ endDateQuery+") ;";

            List<StudentsAttendence> studentAttendenceList;
            SqlCommand cmd = new SqlCommand(retriveTimetableList, con);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    studentAttendenceList = this.getStudentAttendenceList(reader);
                }
                return studentAttendenceList;
            }
            finally
            {
                con.Close();

            }

        }


        public List<StudentsAttendence> GetStudentsMonthlyAttendence(DateTime date, int StudentID)
        {

            string startDateQuery = "SELECT DATEADD(MONTH, DATEDIFF(MONTH, 0, '" + "2019/4/25" + "'), 0)";
            string endDateQuery = "SELECT DATEADD(SECOND, -1, DATEADD(MONTH, 1,  DATEADD(MONTH, DATEDIFF(MONTH, 0,'" + "2019/4/25" + "') , 0) ))";
            string retriveTimetableList = "SELECT a.AttendenceID, t.TimeTableID, s.StudentID, s.FirstName, s.LastName,  t.ClassStartTime, t.ClassEndTime, m.ModuleName, a.Date, a.Condition " +
                "FROM Attendences a " +
                "JOIN Students s On s.StudentID = a.StudentID AND s.StudentID = " + StudentID +
                " JOIN Timetables t On t.TimetableID = a.TimetableID " +
                "JOIN Modules m ON m.ModuleID = t.ModuleID " +
                "WHERE a.Date BETWEEN (" + startDateQuery + ") AND (" + endDateQuery + ") ;";

            List<StudentsAttendence> studentAttendenceList;
            SqlCommand cmd = new SqlCommand(retriveTimetableList, con);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    studentAttendenceList = this.getStudentAttendenceList(reader);
                }
                return studentAttendenceList;
            }
            finally
            {
                con.Close();

            }

        }



        public List<StudentsAttendence> GetStudentsWeeklyAttendence(DateTime date, int StudentID)
        {
            string retriveTimetableList = "SELECT a.AttendenceID, t.TimeTableID, s.StudentID, s.FirstName, s.LastName,  t.ClassStartTime, t.ClassEndTime, m.ModuleName, a.Date, a.Condition " +
                "FROM Attendences a " +
                "JOIN Students s On s.StudentID = a.StudentID AND s.StudentID = " + StudentID +
                " JOIN Timetables t On t.TimetableID = a.TimetableID " +
                "JOIN Modules m ON m.ModuleID = t.ModuleID " +
                "WHERE a.Date BETWEEN (SELECT DATEADD(DAY, 2 - 5,'" + date + "')) AND (SELECT  DATEADD(DAY, 8 - 5, '" + date + "' ))";
            List<StudentsAttendence> studentAttendenceList;
            SqlCommand cmd = new SqlCommand(retriveTimetableList, con);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    studentAttendenceList = this.getStudentAttendenceList(reader);
                }
                return studentAttendenceList;
            }
            finally
            {
                con.Close();

            }

        }

        public List<StudentsAttendence> GetWeeklyAttendence(DateTime date)
        {
            string retriveTimetableList = "SELECT a.AttendenceID, t.TimeTableID, s.StudentID, s.FirstName, s.LastName,  t.ClassStartTime, t.ClassEndTime, m.ModuleName, a.Date, a.Condition " +
                "FROM Attendences a " +
                "JOIN Students s On s.StudentID = a.StudentID " +
                "JOIN Timetables t On t.TimetableID = a.TimetableID " +
                "JOIN Modules m ON m.ModuleID = t.ModuleID " +
                "WHERE a.Date BETWEEN (SELECT DATEADD(DAY, 2 - 5,'" + date + "')) AND (SELECT  DATEADD(DAY, 8 - 5, '" + date + "' ))";
            List<StudentsAttendence> studentAttendenceList;
            SqlCommand cmd = new SqlCommand(retriveTimetableList, con);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    studentAttendenceList = this.getStudentAttendenceList(reader);
                }
                return studentAttendenceList;
            }
            finally
            {
                con.Close();

            }

        }

        public List<StudentsAttendence> getStudentAttendenceList(SqlDataReader reader)
        {
            List<StudentsAttendence> studentAttendenceList = new List<StudentsAttendence>();
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    StudentsAttendence studentAttendence = new StudentsAttendence();
                    studentAttendence.ID = reader.GetInt32(0);
                    studentAttendence.TimetableID = reader.GetInt32(1);
                    studentAttendence.StudentID = reader.GetInt32(2);
                    studentAttendence.FirstName = reader.GetString(3);
                    studentAttendence.LastName = reader.GetString(4);
                    studentAttendence.ClassStartTime = reader.GetTimeSpan(5);
                    studentAttendence.ClassEndTime = reader.GetTimeSpan(6);
                    studentAttendence.ModuleName = reader.GetString(7);
                    studentAttendence.Date = reader.GetDateTime(8).Date;
                    studentAttendence.Condition = reader.GetString(9);
                    studentAttendenceList.Add(studentAttendence);
                }
                reader.NextResult();
            }
            con.Close();
            return studentAttendenceList;
        }


        public List<StudentsAttendence> GetStudentAttendence()
        {
            string retriveTimetableList = "SELECT a.AttendenceID, t.TimeTableID, s.StudentID, s.FirstName, s.LastName,  t.ClassStartTime, t.ClassEndTime, m.ModuleName, a.Date, a.Condition " +
                "FROM Attendences a " +
                "JOIN Students s On s.StudentID = a.StudentID " +
                "JOIN Timetables t On t.TimetableID = a.TimetableID " +
                "JOIN Modules m ON m.ModuleID = t.ModuleID ;";
            List<StudentsAttendence> studentAttendenceList;
            SqlCommand cmd = new SqlCommand(retriveTimetableList, con);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    studentAttendenceList = this.getStudentAttendenceList(reader);
                }
                return studentAttendenceList;
            }
            finally
            {
                con.Close();

            }

        }

        public List<StudentsAttendence> GetStudentAttendence(string groupId, int timetableId)
        {
            string retriveTimetableList = "SELECT a.AttendenceID, t.TimeTableID, s.StudentID, s.FirstName, s.LastName,  t.ClassStartTime, t.ClassEndTime, m.ModuleName, a.Date, a.Condition " +
                "FROM Attendences a " +
                "JOIN Students s ON s.StudentID = a.StudentID " +
                "JOIN Groups g ON g.GroupID = s.GroupID AND g.GroupID = '" + groupId + 
                "' JOIN Timetables t On t.TimetableID = a.TimetableID AND t.TimetableID = " + timetableId +
                "JOIN Modules m ON m.ModuleID = t.ModuleID ;";
            List<StudentsAttendence> studentAttendenceList;
            SqlCommand cmd = new SqlCommand(retriveTimetableList, con);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    studentAttendenceList = this.getStudentAttendenceList(reader);
                }
                return studentAttendenceList;
            }
            finally
            {
                con.Close();

            }

        }

        public Attendence GetAttendence(int studentId, int timetableID)
        {
            string retriveAttendence = "SELECT AttendenceID, TimeTableID, StudentID, Date, Condition FROM Attendence WHERE StudentID = " + studentId + " AND TimetableID = "+ timetableID + " ;";
            SqlCommand cmd = new SqlCommand(retriveAttendence, con);
            try
            {
                Attendence attendence;
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    attendence = this.ReadAttendence(reader);
                }
                con.Close();
                return attendence;
            }
            finally
            {
                con.Close();

            }

        }

        public Attendence GetAttendence(int attendenceID)
        {
            string retriveAttendence = "SELECT AttendenceID, TimeTableID, StudentID, Date, Condition FROM Attendences WHERE AttendenceID = " + attendenceID + " ;";
            SqlCommand cmd = new SqlCommand(retriveAttendence, con);
            try
            {
                Attendence attendence;
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    attendence = this.ReadAttendence(reader);
                }
                con.Close();
                return attendence;
            }
            finally
            {
                con.Close();

            }

        }

        public StudentsAttendence GetStudentAttendence(int attendenceID)
        {
            string retriveTimetableList = "SELECT a.AttendenceID, t.TimeTableID, s.StudentID, s.FirstName, s.LastName,  t.ClassStartTime, t.ClassEndTime, m.ModuleName, a.Date, a.Condition " +
                "FROM Attendences a "+
                "JOIN Students s On s.StudentID = a.StudentID AND a.AttendenceID = " + attendenceID + 
                " JOIN Timetables t On t.TimetableID = a.TimetableID " +
                "JOIN Modules m ON m.ModuleID = t.ModuleID ;";

            SqlCommand cmd = new SqlCommand(retriveTimetableList, con);

            try
            {
                StudentsAttendence studentAttendence;
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    studentAttendence = this.ReadStudentAttendence(reader);
                }
                con.Close();
                return studentAttendence;
            }
            finally
            {
                con.Close();
            }

        }

        public StudentsAttendence GetStudentAttendence(int studentId, int timetableID)
        {
            string retriveTimetableList = "SELECT a.AttendenceID, t.TimeTableID, s.StudentID, s.FirstName, s.LastName,  t.ClassStartTime, t.ClassEndTime, m.ModuleName, a.Date, a.Condition " +
                "FROM Attendences a " +
                "JOIN Students s On s.StudentID = a.StudentID AND StudentID = " + studentId +
                " JOIN Timetables t On t.TimetableID = a.TimetableID AND TimetableID = " +timetableID +
                " JOIN Modules m ON m.ModuleID = t.ModuleID ;";

            SqlCommand cmd = new SqlCommand(retriveTimetableList, con);

            try
            {
                StudentsAttendence studentAttendence;
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    studentAttendence = this.ReadStudentAttendence(reader);
                }
                con.Close();
                return studentAttendence;
            }
            finally
            {
                con.Close();
            }

        }

        public List<Attendence> GetAttendence()
        {
            string retriveStudentList = "SELECT TimeTableID, StudentID, Date, Condition FROM Attendence ;";
            List<Attendence> attendenceList = new List<Attendence>();
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
                            Attendence attendence = new Attendence();
                            attendence.TimetableID = reader.GetInt32(0);
                            attendence.StudentID = reader.GetInt32(0);
                            attendence.Date = reader.GetDateTime(3).Date;
                            attendence.Condition = reader.GetString(4);
                            attendenceList.Add(attendence);
                        }
                        reader.NextResult();
                    }
                    con.Close();
                }
                return attendenceList;
            }
            finally
            {
                con.Close();

            }

        }

        public List<StudentsAttendence> GetEachStudentAttendence(int StudentID)
        {
            string retriveTimetableList = "SELECT a.AttendenceID t.TimeTableID, s.StudentID, s.FirstName, s.LastName,  t.ClassStartTime, t.ClassEndTime, m.ModuleName, a.Date, a.Condition " +
                "FROM Attendence a " +
                "JOIN Students s On s.StudentID = a.StudentID AND s.StudentID = " + StudentID + 
                " JOIN Timetables t On t.TimetableID = a.TimetableID " +
                "JOIN Modules m ON m.ModuleID = t.ModuleID ;";
            List<StudentsAttendence> studentAttendenceList;
            SqlCommand cmd = new SqlCommand(retriveTimetableList, con);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    studentAttendenceList = this.getStudentAttendenceList(reader);
                }
                return studentAttendenceList;
            }
            finally
            {
                con.Close();

            }
        }

        public List<StudentsAttendence> GetGroupAttendence(int GroupID, DateTime dateTime)
        {
            string retriveTimetableList = "SELECT a.AttendenceID, t.TimeTableID, s.StudentID, s.FirstName, s.LastName,  t.ClassStartTime, t.ClassEndTime, m.ModuleName, a.Date, a.Condition " +
                "FROM Attendence a " +
                "JOIN Students s On s.StudentID = a.StudentID " +
                "JOIN Group g On s.GroupID = g.GroupID AND g.GroupID = " + GroupID +
                " JOIN Timetables t On t.TimetableID = a.TimetableID AND a.Date = " + dateTime +
                " JOIN Modules m ON m.ModuleID = t.ModuleID ;";
            List<StudentsAttendence> studentAttendenceList;
            SqlCommand cmd = new SqlCommand(retriveTimetableList, con);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    studentAttendenceList = this.getStudentAttendenceList(reader);
                }
                return studentAttendenceList;
            }
            finally
            {
                con.Close();

            }
        }




        public void UpdateAttendence(Attendence attendence)
        {
            string updateQuery = "UPDATE Attendences " +
                "SET Condition = '" + attendence.Condition + "', Date = '" + attendence.Date + "', StudentID = " + attendence.StudentID + ", TimetableID = " + attendence.TimetableID + " WHERE AttendenceID = " + attendence.AttendenceID + " ;";
            ExecuteQuery(updateQuery);
        }


        public void DeleteAttendence(int attendenceID)
        {
            string delete = "DELETE Attendences where ID = " + attendenceID + " ; ";
            ExecuteQuery(delete);
        }
    }
}