namespace SwimMeetTracker.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AthleteTeam
    {
        [Key]
        public int ATID { get; set; }

        public int AthleteID { get; set; }

        public int TeamID { get; set; }

        public virtual Athlete Athlete { get; set; }

        public virtual Team Team { get; set; }
    }
}
