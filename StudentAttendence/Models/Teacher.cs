using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentAttendence.Models
{


    public class Teacher
    {
        [Required]
        [Key]
        public int TeacherID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string Contact { get; set; }

        public bool Status { get; set; }


        public Teacher(){
            this.Status = true;
        }
        public Teacher(int teacherID, string firstName, string lastName, string email, string contact, bool status, DateTime hireDate)
        {
            TeacherID = teacherID;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Contact = contact;
            Status = status;
            HireDate = hireDate;
        }

        public Teacher(string firstName, string lastName, string email, string contact, DateTime hireDate)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Contact = contact;
            HireDate = hireDate;
            Status = true;
        }

        [Required]
        public DateTime HireDate { get; set; }

        public virtual List<TeacherRole> TeacherRole { get; set; }
    }
}