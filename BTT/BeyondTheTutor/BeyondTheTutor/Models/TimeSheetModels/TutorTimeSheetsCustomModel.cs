namespace BeyondTheTutor.Models.TimeSheetModels
{
    using System.Collections.Generic;
    using BeyondTheTutor.Models;

    public class TutorTimeSheetCustomModel
    {
        public Tutor tutor { get; set; }
        public Dictionary<byte, string> days { get; set; }
        public Dictionary<byte, string> months { get; set; }

        public List<TimeSheet> TimeSheets { get; set; }
        //public List<Day> Days { get; set; }
        //public List<WorkHour> WorkHours { get; set; }
        //public TimeSheet SelectedTS { get; set; }
        //public Day SelectedD { get; set; }

        public string DisplayMode { get; set; }
    }
}