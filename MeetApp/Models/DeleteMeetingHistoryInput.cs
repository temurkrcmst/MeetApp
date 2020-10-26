using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetApp.Models
{
    public class DeleteMeetingHistoryInput
    {

        public string FilterCol { get; set; }
        public string FilterVal { get; set; }
    }
}