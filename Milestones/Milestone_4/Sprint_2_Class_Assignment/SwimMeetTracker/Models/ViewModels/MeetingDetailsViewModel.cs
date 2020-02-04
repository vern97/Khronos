using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SwimMeetTracker.Models.ViewModels
{
    public class MeetingDetailsViewModel
    {
        public MeetingDetailsViewModel(Meeting mt)
        {
            //meeting personal details
            var details = mt.Races.OrderBy(t => t.TypeOfMeeting).ThenBy(t => t.FinishTime)
            .Select(m => new
            {
                theTeam = m.Athlete.AthleteTeams.FirstOrDefault().Team.Name,
                FName = m.Athlete.FirstName,
                LName = m.Athlete.LastName,
                EventType = m.TypeOfMeeting,
                CompletedIn = m.FinishTime
            }) ;

            eName = mt.Name;
            eDate = mt.MeetingDate;
            Names = details.Select(i => i.FName + " " + i.LName).ToList();
            EventTypes = details.Select(i => i.EventType).ToList();
            CompletedIns = details.Select(i => i.CompletedIn).ToList();
            Teams = details.Select(i => i.theTeam).ToList();
        }

        [DisplayName("Meet")]
        public string eName { get; set; }

        [DisplayName("Event Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime eDate { get; set; }


        [DisplayName("Athlete Name")]
        public List<string> Names { get; set; }


        [DisplayName("Team")]
        public List<string> Teams { get; set; }

        [DisplayName("Event")]
        public List<string> EventTypes { get; set; }

        [DisplayName("Completed In (s)")]
        public List<float> CompletedIns { get; set; }

    }
}