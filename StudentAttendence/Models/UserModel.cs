using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentAttendence.Models
{
    public class UserModel
    {
        public string UserID { get; set; }

        public string RoleID { get; set; }
        public UserModel(string UserID, string RoleID){
            this.UserID = UserID;
            this.RoleID = RoleID;
        }

        public UserModel() { }
        
    }
}