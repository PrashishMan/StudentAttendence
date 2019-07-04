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

        public void CreateTimetable(Timetable timetable)
        {
            string createQuery = "INSERT INTO Timetables (ClassStartTime, ClassEndTime, Day, Room, Year, ClassType, ModuleID, SemesterID, Status)" + 
                "VALUES('" + timetable.ClassStartTime + "','" + timetable.ClassEndTime + "','" + timetable.Day + "','" + timetable.Room + "','" + timetable.Year + "','" + timetable.ClassType + "','" + timetable.ModuleID + "','" + timetable.SemesterID + "', 1)";
            ExecuteQuery(createQuery);
        }

        public Timetable ReadTimetable(SqlDataReader reader)
        {
            Timetable timetable = new Timetable();
            while (reader.Read())
            {
                timetable.TimeTableId = reader.GetInt32(0);
                timetable.ClassStartTime = reader.GetTimeSpan(1);
                timetable.ClassEndTime = reader.GetTimeSpan(2);
                timetable.Day = reader.GetString(3);
                timetable.Room = reader.GetString(4);
                timetable.Year = reader.GetInt32(5);
                timetable.ClassType = reader.GetString(6);
                timetable.ModuleID = reader.GetInt32(7);
                timetable.SemesterID = reader.GetInt32(8);

            }
            return timetable;
        }




        public ModuleTimetableBridge ReadModuleTimetable(SqlDataReader reader)
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
                moduleTimetable.ClassType = reader.GetString(6);
                moduleTimetable.ModuleID = reader.GetInt32(7);
                moduleTimetable.ModuleName = reader.GetString(8);
                moduleTimetable.FacultyName = reader.GetString(9);
                moduleTimetable.Semester = reader.GetInt32(10);
            }
            return moduleTimetable;
        }

        public List<TeacherTeachingHrs> GetTeacherTeachingHrs()
        {
            string retriveTeacherTeachingList = "SELECT t.FirstName, t.LastName, t.Email, t.Contact, SUM(DATEDIFF(MINUTE, tm.ClassStartTime, tm.ClassEndTime)),  m.ModuleName, r.RoleName " +
                "FROM Timetables tm " +
                "JOIN TeacherRoles tr ON tr.ModuleID = tm.ModuleID " +
                "JOIN Teachers t ON tr.TeacherID = t.TeacherID " +                
                "JOIN Roles r ON r.RoleID = tr.RoleID AND tm.ClassType = r.RoleName " +
                "JOIN Modules m On tm.ModuleID = m.ModuleID " +
                "GROUP BY t.FirstName, t.LastName, t.Email, t.Contact, m.ModuleName, r.RoleName ; " ;
            List<TeacherTeachingHrs> teacherTeachingHrsList = new List<TeacherTeachingHrs>();
            SqlCommand cmd = new SqlCommand(retriveTeacherTeachingList, con);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.HasRows)
                    {

                        while (reader.Read())
                        {

                            string FirstName = reader.GetString(0);
                            string LastName = reader.GetString(1);
                            string Email = reader.GetString(2);
                            string Contact = reader.GetString(3);
                            int TotalMinute = reader.GetInt32(4);
                            string ModuleName = reader.GetString(5);
                            string RoleName = reader.GetString(6);


                            TeacherTeachingHrs moduleTimetable = new TeacherTeachingHrs(FirstName, LastName, Email, Contact, TotalMinute, ModuleName, RoleName);
                            teacherTeachingHrsList.Add(moduleTimetable);
                        }
                        reader.NextResult();
                    }
                    con.Close();
                }
                return teacherTeachingHrsList;
            }
            finally
            {
                con.Close();

            }

            return teacherTeachingHrsList;
        }

            public List<ModuleTimetableBridge> GetModuleTimetable()
        {
            string retriveTimetableList = "SELECT t.TimeTableID, t.ClassStartTime, t.ClassEndTime, t.Day, t.Room, t.Year, t.ClassType, m.ModuleID, m.ModuleName, f.FacultyName, s.SemesterNo " +
                "FROM Timetables t " +
                "JOIN Semesters s ON s.SemesterID = t.SemesterID " +
                "JOIN Modules m On t.ModuleID = m.ModuleID AND t.Status = 1 " + 
                "JOIN Faculties f ON m.FacultyID = f.FacultyID ;";
            List<ModuleTimetableBridge> moduleTimetableList = new List<ModuleTimetableBridge>();
            SqlCommand cmd = new SqlCommand(retriveTimetableList, con);
            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            
                            int TimeTableId = reader.GetInt32(0);
                            TimeSpan ClassStartTime = reader.GetTimeSpan(1);
                            TimeSpan ClassEndTime = reader.GetTimeSpan(2);
                            string Day = reader.GetString(3);
                            string Room = reader.GetString(4);
                            int Year = reader.GetInt32(5);
                            string ClassType = reader.GetString(6);
                            int ModuleID = reader.GetInt32(7);
                            string ModuleName = reader.GetString(8);
                            string FacultyName = reader.GetString(9);
                            int Semester = reader.GetInt32(10);

                            ModuleTimetableBridge moduleTimetable = new ModuleTimetableBridge(TimeTableId, ClassStartTime, ClassEndTime, Day, Room, Year, ModuleID, ModuleName, ClassType, FacultyName, Semester);
                            moduleTimetableList.Add(moduleTimetable);
                        }
                        reader.NextResult();
                    }
                    con.Close();
                }
                return moduleTimetableList;
            }
            finally
            {
                con.Close();

            }

        }

        public List<Timetable> GetTimetable()
        {
            string retriveStudentList = "SELECT TimeTableID, ClassStartTime, ClassEndTime, Day, Room, Year, ClassType, ModuleID, SemesterID FROM Timetables WHERE Status = 1 ;";
            List<Timetable> timetableList = new List<Timetable>();
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
                            Timetable timetable = new Timetable();
                            timetable.TimeTableId = reader.GetInt32(0);
                            timetable.ClassStartTime = reader.GetTimeSpan(1);
                            timetable.ClassEndTime = reader.GetTimeSpan(2);
                            timetable.Day = reader.GetString(3);
                            timetable.Room = reader.GetString(4);
                            timetable.Year = reader.GetInt32(5);
                            timetable.ClassType = reader.GetString(6);
                            timetable.ModuleID = reader.GetInt32(7);
                            timetable.SemesterID = reader.GetInt32(8);
                            timetableList.Add(timetable);
                        }
                        reader.NextResult();
                    }
                    con.Close();
                }
                return timetableList;
            }
            finally
            {
                con.Close();

            }

        }

        public ModuleTimetableBridge GetModuleTimetable(int TimetableID)
        {
            string retriveModuleTimetable = "SELECT t.TimeTableID, t.ClassStartTime, t.ClassEndTime, t.Day, t.Room, t.Year, t.ClassType, m.ModuleID, m.ModuleName, f.FacultyName, s.SemesterNo " +
                "FROM Timetables t " +
                "JOIN Semesters s ON s.SemesterID = t.SemesterID " +
                "JOIN Modules m On t.ModuleID = m.ModuleID AND t.TimetableID = " + TimetableID + " AND t.Status = 1" +
                "JOIN Faculties f ON m.FacultyID = f.FacultyID ;";

            SqlCommand cmd = new SqlCommand(retriveModuleTimetable, con);
            ModuleTimetableBridge moduleTimetable = new ModuleTimetableBridge();
            try
            {
                con.Open();
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    moduleTimetable = this.ReadModuleTimetable(oReader);
                    con.Close();
                }
                return moduleTimetable;
            }
            finally
            {
                con.Close();

            }
        }

        public Timetable GetTimetable(int timetableID)
        {
            string retriveString = "SELECT TimeTableID, ClassStartTime, ClassEndTime," +
                " Day, Room, Year, ClassType, ModuleID, SemesterID FROM Timetables WHERE Status = 1 AND TimeTableID = " + timetableID + "; ";

            SqlCommand cmd = new SqlCommand(retriveString, con);
            Timetable timetable = new Timetable();
            try
            {
                con.Open();
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    timetable = this.ReadTimetable(oReader);
                    con.Close();
                }
                return timetable;
            }
            finally
            {
                con.Close();

            }
        }


        public void UpdateTimetable(Timetable timetable)
        {
            string updateQuery = "UPDATE Timetables " +
                "SET ClassStartTime = '" + timetable.ClassStartTime + "', ClassEndTime = '" + timetable.ClassEndTime + "', Day = '" + timetable.Day + "', Room = '" + timetable.Room + "', Year = '" + timetable.Year + "', ClassType = '" + timetable.ClassType + "', SemesterID = '" + timetable.SemesterID + "' WHERE TimetableID = '" + timetable.TimeTableId + "' ;";
            ExecuteQuery(updateQuery);
        }


        public void DeleteTimetable(int id)
        {
            string deleteQuery = "UPDATE Timetables SET STATUS = 0 where TimetableID = '" + id + "' ;";
            ExecuteQuery(deleteQuery);
        }
    }
}