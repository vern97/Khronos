using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeyondTheTutor.Models.ZoomModels
{
    public class Users
    {
        public int page_count { get; set; }
        public int page_size { get; set; }
        public List<UserInfo> users { get; set; }
    }
}