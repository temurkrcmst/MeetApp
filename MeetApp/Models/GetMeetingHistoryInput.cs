using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetApp.Models
{
    public class GetMeetingHistoryInput
    {
        public string Column { get; set; }
        public string ColumnType { get; set; }
        public string Value { get; set; }
        public string Statement { get; set; }
    }
}