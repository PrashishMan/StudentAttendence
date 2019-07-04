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

        public void CreateModule(Module module)
        {
            string createQuery = "INSERT INTO Modules (ModuleName, ModuleType, Credit, FacultyID, SemesterID, Status)" +
                "VALUES('" + module.ModuleName + "','" + module.ModuleType + "','" + module.Credit + "','" + module.FacultyID + "','" + module.SemesterID + "', 1)";
            ExecuteQuery(createQuery);
        }

        public Module ReadModule(SqlDataReader reader)
        {
            Module module = new Module();
            while (reader.Read())
            {
                module.ModuleID = reader.GetInt32(0);
                module.ModuleName = reader.GetString(1);
                module.ModuleType = reader.GetString(2);
                module.Credit = reader.GetInt32(3);
                module.FacultyID = reader.GetInt32(4);
            }
            return module;
        }

        public FacultyModule ReadFacultyModule(SqlDataReader reader)
        {
            FacultyModule facultyModule = new FacultyModule();
            while (reader.Read())
            {
                facultyModule.ModuleID = reader.GetInt32(0);
                facultyModule.ModuleName = reader.GetString(1);
                facultyModule.ModuleType = reader.GetString(2);
                facultyModule.Credit = reader.GetInt32(3);
                facultyModule.FacultyID = reader.GetInt32(4);
                facultyModule.FacultyName = reader.GetString(5);


            }
            return facultyModule;
        }

        public List<Module> GetModule()
        {
            string retriveStudentList = "SELECT ModuleID, ModuleName, ModuleType, Credit, FacultyID FROM Modules WHERE Status = 1 ;";
            List<Module> moduleList = new List<Module>();
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
                            Module module = new Module();
                            module.ModuleID = reader.GetInt32(0);
                            module.ModuleName = reader.GetString(1);
                            module.ModuleType = reader.GetString(2);
                            module.Credit = reader.GetInt32(3);
                            module.FacultyID = reader.GetInt32(4);

                            moduleList.Add(module);
                        }
                        reader.NextResult();
                    }
                    con.Close();
                }
                return moduleList;
            }
            finally
            {
                con.Close();

            }

        }

        public List<FacultyModule> GetFacultyModule()
        {
            string retriveStudentList = "SELECT m.ModuleID, m.SemesterID, m.ModuleName, m.ModuleType, m.Credit, m.FacultyID, f.FacultyName FROM Modules m JOIN Faculties f ON m.FacultyID = f.FacultyID AND m.Status = 1 ;";
            List<FacultyModule> facultyModuleList = new List<FacultyModule>();
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
                            FacultyModule facultyModule = new FacultyModule();
                            facultyModule.ModuleID = reader.GetInt32(0);
                            facultyModule.SemesterNo = reader.GetInt32(1);
                            facultyModule.ModuleName = reader.GetString(2);
                            facultyModule.ModuleType = reader.GetString(3);
                            facultyModule.Credit = reader.GetInt32(4);
                            facultyModule.FacultyID = reader.GetInt32(5);
                            facultyModule.FacultyName = reader.GetString(6);

                            facultyModuleList.Add(facultyModule);
                        }
                        reader.NextResult();
                    }
                    con.Close();
                }
                return facultyModuleList;
            }
            finally
            {
                con.Close();

            }

        }


        public Module GetModule(int moduleId)
        {
            string retriveString = "SELECT ModuleID, ModuleName, ModuleType, Credit, FacultyID from Modules WHERE ModuleID = '" + moduleId + "' ;";

            SqlCommand cmd = new SqlCommand(retriveString, con);
            Module module = new Module();
            try
            {
                con.Open();
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    module = this.ReadModule(oReader);
                    con.Close();
                }
                return module;
            }
            finally
            {
                con.Close();

            }
        }

        public FacultyModule GetFacultyModule(int moduleId)
        {
            string retriveString = "SELECT m.ModuleID, m.ModuleName, m.ModuleType, m.Credit, m.FacultyID, f.FacultyName from Modules m JOIN Faculties f ON m.FacultyID = f.FacultyID AND m.ModuleID = '" + moduleId + "' ;";

            SqlCommand cmd = new SqlCommand(retriveString, con);
            FacultyModule facultyModule = new FacultyModule();
            try
            {
                con.Open();
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    facultyModule = this.ReadFacultyModule(oReader);
                    con.Close();
                }
                return facultyModule;
            }
            finally
            {
                con.Close();

            }
        }


        public void UpdateModule(Module module)
        {
            string updateQuery = "UPDATE Modules " +
                "SET ModuleName = '" + module.ModuleName + "', ModuleType = '" + module.ModuleType + "', Credit = '" + module.Credit + "', FacultyID = '" + module.FacultyID + "' WHERE moduleID = '" + module.ModuleID + "' ;";
            ExecuteQuery(updateQuery);
        }


        public void DeleteModule(int id)
        {
            string deleteQuery = "UPDATE Modules SET STATUS = 0 where ModuleID = '" + id + "' ;";
            ExecuteQuery(deleteQuery);
        }
    }
}