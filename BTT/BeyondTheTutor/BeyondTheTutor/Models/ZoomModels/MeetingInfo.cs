using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeyondTheTutor.Models.ZoomModels
{
    public class MeetingInfo
    {
        public int duration { get; set; }
        public string host_id { get; set; }
        public long id { get; set; }
        public string join_url { get; set; }
        public string topic { get; set; }
        public string start_time { get; set; }
        public string uuid { get; set; }
    }
}