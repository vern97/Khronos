namespace BeyondTheTutor.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FinalGrade
    {
        public int ID { get; set; }

        public DateTime RecordedDate { get; set; }

        [Required]
        [StringLength(50)]
        public string ClassName { get; set; }

        public double Grade { get; set; }

        public int UserID { get; set; }

        public virtual BTTUser BTTUser { get; set; }
    }
}
