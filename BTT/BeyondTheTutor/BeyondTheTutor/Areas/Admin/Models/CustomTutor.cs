namespace BeyondTheTutor.Areas.Admin.Models
{
    using System.Collections.Generic;

    public class CustomTutor
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string vNumber { get; set; }
        public bool adminApproved { get; set; }
        public string role { get; set; }
    }
}
