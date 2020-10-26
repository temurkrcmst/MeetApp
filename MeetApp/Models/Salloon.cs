using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetApp.Models
{
    public class Salloon
    {
        public string ID { get; set; }

        public string Branch { get; set; }

        public int Capacity { get; set; }

        public bool ProjectionStatus { get; set; }

        public bool InternetStatus { get; set; }

        public bool AirConditioningStatus { get; set; }

        public bool ProvisionsStatus { get; set; }
        public string  SalloonName { get; set; }



    }

}