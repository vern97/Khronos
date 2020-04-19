namespace BeyondTheTutor.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TutorSchedule")]
    public partial class TutorSchedule
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        [StringLength(50)]
        public string ThemeColor { get; set; }

        public bool? IsFullDay { get; set; }

        public int TutorID { get; set; }

        public virtual Tutor Tutor { get; set; }
    }
}