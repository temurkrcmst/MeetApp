using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetApp.Models
{
    public class UpdateUserInput
    {
        public string FilterVal { get; set; } // tek bir sorgu ile sorgu işlemi yapmadan dinamik olarak filtreleme işlemi yaparız BsonValue ile, kolona ait değeri otomatik olarak alır
        public string FilterCol { get; set; }
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