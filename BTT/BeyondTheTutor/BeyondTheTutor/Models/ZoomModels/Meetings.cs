using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeyondTheTutor.Models.ZoomModels
{
    public class Meetings
    {
        public int page_count { get; set; }
        public int page_number { get; set; }
        public int page_size { get; set; }
        public int total_records { get; set; }
        public List<MeetingInfo> meetings { get; set; }
    }
}