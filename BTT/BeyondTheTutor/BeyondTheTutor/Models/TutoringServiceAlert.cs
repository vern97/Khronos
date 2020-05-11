namespace BeyondTheTutor.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TutoringServiceAlert
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public DateTime EndTime { get; set; }

        public int TutorID { get; set; }

        public virtual Tutor Tutor { get; set; }
    }
}