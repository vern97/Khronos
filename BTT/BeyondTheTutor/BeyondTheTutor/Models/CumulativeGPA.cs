namespace BeyondTheTutor.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CumulativeGPA
    {
        public int ID { get; set; }

        public DateTime RecordedDate { get; set; }

        [Column("CumulativeGPA")]
        public double CumulativeGPA1 { get; set; }

        public int UserID { get; set; }

        public virtual BTTUser BTTUser { get; set; }
    }
}
