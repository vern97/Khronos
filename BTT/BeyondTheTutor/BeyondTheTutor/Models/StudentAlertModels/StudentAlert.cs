namespace BeyondTheTutor.Models.StudentAlertModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class StudentAlert
    {
        public int ID { get; set; }

        public DateTime TimeStamp { get; set; }

        [Required]
        [StringLength(100)]
        public string Subject { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Message { get; set; }

        public DateTime Expiration { get; set; }

        public int AdminID { get; set; }

        public virtual BTTUser BTTUser { get; set; }
    }
}
