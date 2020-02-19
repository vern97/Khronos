using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeyondTheTutor.Models.ViewModels
{
    public class TutorSchedulesViewModel
    {
        public TutorSchedulesViewModel(TutorSchedule tutorSchedule)
        {

            title = tutorSchedule.Description;
            start = tutorSchedule.StartTime;
            end = tutorSchedule.EndTime;
        }


        public string title { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
    }
}