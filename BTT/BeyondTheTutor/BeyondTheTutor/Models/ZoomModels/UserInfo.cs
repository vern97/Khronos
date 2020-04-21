using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeyondTheTutor.Models.ZoomModels
{
    public class UserInfo
    {
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string id { get; set; }
        public long pmi { get; set; }
        public string status { get; set; }
    }
}