namespace BeyondTheTutor.Models.TimeSheetModels
{
    using System.Collections.Generic;

    public class TutorTimeSheetCustomModel
    {
        public Tutor tutor { get; set; }
        public Dictionary<byte, string> days { get; set; }
        public Dictionary<byte, string> months { get; set; }

        public List<TimeSheet> TimeSheets { get; set; }

        public TimeSheet TimeSheetVM { get; set; }
        public Day DayVM { get; set; }

        public string DisplayMode { get; set; }
    }
}