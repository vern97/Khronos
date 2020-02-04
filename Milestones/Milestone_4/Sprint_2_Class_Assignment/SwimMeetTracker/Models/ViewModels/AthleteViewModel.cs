using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SwimMeetTracker.Models.ViewModels
{
    public class AthleteViewModel
    {
        public AthleteViewModel(Athlete ath)
        {
            //Athlete personal details
            var athdets = ath.AthleteTeams
                                .Select(AID => new 
                                    { FName = AID.Athlete.FirstName, 
                                    LName = AID.Athlete.LastName, 
                                    DOB = AID.Athlete.DateOfBirth, 
                                    Team = AID.Team.Name, 
                                    FNameCoach = AID.Team.Coach.FirstName, 
                                    LNameCoach = AID.Team.Coach.LastName});

            Name = athdets.Select(i => i.FName + " " + i.LName).ToList();
            DateOfBirth = athdets.Select(i => i.DOB.ToShortDateString()).ToList();
            Team = athdets.Select(i => i.Team).ToList();
            CoachName = athdets.Select(i => i.FNameCoach + " " + i.LNameCoach + " (" + i.Team + ")").ToList();
            AID = ath.AID;

            //Athlete Race Details 
            //
            NoEvents = 0;
            var meets = ath.Races.OrderByDescending(m => m.Meeting.MeetingDate)
            .Select(AID => new
            {
                MeetName = AID.Meeting.Name,
                MeetDate = AID.Meeting.MeetingDate,
                EventType = AID.TypeOfMeeting,
                CompletedIn = AID.FinishTime,
                MID = AID.MeetingID
            });

            MeetNames = meets.Select(i => i.MeetName).ToList();
            MeetDates = meets.Select(i => i.MeetDate.ToShortDateString()).ToList();
            CompletedIns = meets.Select(i => i.CompletedIn).ToList();
            EventTypes = meets.Select(i => i.EventType).ToList();
            FK_MIDs = meets.Select(i => i.MID).ToList();
        }

        public int AID { get; set; }

        public int NoEvents { get; set; }

        [DisplayName("Athlete Name")]
        public List<string> Name { get; set; }

        [DisplayName("Date of Birth")]
        public List<string> DateOfBirth { get; set; }

        [DisplayName("Team(s)")]
        public List<string> Team { get; set; }

        [DisplayName("Coach(es)")]
        public List<string> CoachName { get; set; }

        //Athlete Race Details
        [DisplayName("Meeting")]
        public List<string> MeetNames { get; set; }

        public List<int> FK_MIDs { get; set; }

        [DisplayName("Date")]
        public List<string> MeetDates { get; set; }

        [DisplayName("Event")]
        public List<string> EventTypes { get; set; }

        [DisplayName("Completed In (s)")]
        public List<float> CompletedIns { get; set; }
    }
}