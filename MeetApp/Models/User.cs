using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetApp.Models
{
    public class User
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Degree { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Authority { get; set; }
        public string State { get; set; }
        public string BranchID { get; set; }
    }
}