using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetApp.Models
{
    public class UpdateBranchInput
    {
        public string FilterCol { get; set; }
        public string FilterVal { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}