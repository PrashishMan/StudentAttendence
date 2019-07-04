using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentAttendence.Models
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }

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

        public DateTime EnrolledDate { get; set; }

        public bool Status { get; set; }


        public Student() {
            this.Status = true;
        }

        public Student(string firstName, string lastName, string email, string contact, DateTime enrolledDate, string groupID)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Contact = contact;
            this.EnrolledDate = enrolledDate;
            GroupID = groupID;
            Status = true;
        }

        public Student(int studentID, string firstName, string lastName, string email, string contact, bool Status, string groupID)
        {
            StudentID = studentID;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Contact = contact;
            Status = Status;
            GroupID = groupID;
        }

        public Student(string firstName, string lastName, string email, string contact, string groupID)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Contact = contact;
            GroupID = groupID;
            Status = true;
        }

        [ForeignKey("Group")]
        public string GroupID { get; set; }

        public virtual Group Group { get; set; }

        
    }
}