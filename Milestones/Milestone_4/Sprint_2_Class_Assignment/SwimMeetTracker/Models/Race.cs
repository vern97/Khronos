namespace SwimMeetTracker.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Race
    {
        [Key]
        public int RID { get; set; }

        public int AthleteID { get; set; }

        public int MeetingID { get; set; }

        public float FinishTime { get; set; }

        [Required]
        [StringLength(50)]
        public string TypeOfMeeting { get; set; }

        public virtual Athlete Athlete { get; set; }

        public virtual Meeting Meeting { get; set; }
    }
}
