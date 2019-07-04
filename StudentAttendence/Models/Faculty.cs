using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace StudentAttendence.Models
{
    public class Faculty
    {

        [Key]
        public int FacultyID { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string FacultyName { get; set; }

        [Required]
        public bool Status { get; set; }

        List<Faculty> list = new List<Faculty>();


        public Faculty() {
            this.Status = true;
        }
        public Faculty(int FacultyID, string FacultyName) {
            this.FacultyID = FacultyID;
            this.FacultyName = FacultyName;
            this.Status = true;
        }

        public List<Faculty> List(DataTable dt)
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Faculty fac = new Faculty();
                fac.FacultyID = Convert.ToInt32(dt.Rows[i]["FacultyID"]);
                fac.FacultyName = dt.Rows[i]["FacultyName"].ToString();
                list.Add(fac);
            }
            return list;

        }

    }
}