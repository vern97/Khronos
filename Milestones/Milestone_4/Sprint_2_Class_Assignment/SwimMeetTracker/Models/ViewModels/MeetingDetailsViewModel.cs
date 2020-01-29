﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SwimMeetTracker.Models.ViewModels
{
    public class MeetingDetailsViewModel
    {
        public MeetingDetailsViewModel(Meeting mt)
        {
            //meeting personal details
            var details = mt.Races.OrderBy(t => t.TypeOfMeeting)
            .Select(m => new
            {
                theTeam = m.Athlete.AthleteTeams.FirstOrDefault().Team.Name,
                FName = m.Athlete.FirstName,
                LName = m.Athlete.LastName,
                EventType = m.TypeOfMeeting,
                CompletedIn = m.FinishTime
            }) ;

            Names = details.Select(i => i.FName + " " + i.LName).ToList();
            EventTypes = details.Select(i => i.EventType).ToList();
            CompletedIns = details.Select(i => i.CompletedIn).ToList();
            Teams = details.Select(i => i.theTeam).ToList();
        }

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