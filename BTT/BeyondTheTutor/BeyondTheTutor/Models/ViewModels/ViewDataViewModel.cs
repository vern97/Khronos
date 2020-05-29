using BeyondTheTutor.Models.TimeSheetModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeyondTheTutor.Models.ViewModels
{
    public class ViewDataViewModel
    {
        public List<TimeSheet> TimeSheets { get; set; }
        public List<TutoringAppt> Appts { get; set; }
        public List<Student> Students { get; set; }

        public Dictionary<string, int> courseAptFreq = new Dictionary<string, int>();
        public Dictionary<string, int> courseAptFreq_dated = new Dictionary<string, int>();

    }
}