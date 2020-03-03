namespace BeyondTheTutor.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TutoringAppt
    {
        public int ID { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        [StringLength(50)]
        public string TypeOfMeeting { get; set; }

        public int ClassID { get; set; }

        [Required]
        [StringLength(50)]
        public string Length { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [Column(TypeName = "text")]
        public string Note { get; set; }

        public int StudentID { get; set; }

        public int? TutorID { get; set; }

        public virtual Class Class { get; set; }

        public virtual Student Student { get; set; }

        public virtual Tutor Tutor { get; set; }
    }
}
