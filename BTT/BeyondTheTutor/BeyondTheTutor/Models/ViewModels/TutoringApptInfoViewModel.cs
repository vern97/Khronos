using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeyondTheTutor.Models.ViewModels
{
    public class TutoringApptInfoViewModel
    {
        public TutoringApptInfoViewModel(TutoringAppt tutoringAppt)
        {
            ID = tutoringAppt.ID;
            StartTime = tutoringAppt.StartTime;
            EndTime = tutoringAppt.EndTime;
            TypeOfMeeting = tutoringAppt.TypeOfMeeting;
            ClassID = tutoringAppt.ClassID;
            Class = tutoringAppt.Class.Name;
            Length = tutoringAppt.Length;
            Note = tutoringAppt.Note;
            StudentID = tutoringAppt.StudentID;
            StudentName = tutoringAppt.Student.BTTUser.FirstName + " " + tutoringAppt.Student.BTTUser.LastName;
        }

        public int ID { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string TypeOfMeeting { get; set; }

        public int ClassID { get; set; }

        public string Class { get; set; }

        public string Length { get; set; }

        public string Note { get; set; }

        public int StudentID { get; set; }

        public string StudentName { get; set; }
    }
}