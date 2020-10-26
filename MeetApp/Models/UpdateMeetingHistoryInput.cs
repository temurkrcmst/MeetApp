using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetApp.Models
{
    public class UpdateMeetingHistoryInput
    {
        public string FilterCol { get; set; }
        public string FilterVal { get; set; }
        public string Title { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public int ParticipantNumber { get; set; }
        public string Salloon { get; set; }
        public string Moderator { get; set; }
        public string BranchID { get; set; }
    }
}