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
        }

        public int AID { get; set; }

        [DisplayName("Athlete Name")]
        public List<string> Name { get; set; }

        [DisplayName("Date of Birth")]
        public List<string> DateOfBirth { get; set; }

        [DisplayName("Team(s)")]
        public List<string> Team { get; set; }

        [DisplayName("Coach(es)")]
        public List<string> CoachName { get; set; }
    }
}