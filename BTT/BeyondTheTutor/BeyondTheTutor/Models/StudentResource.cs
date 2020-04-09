namespace BeyondTheTutor.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class StudentResource
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Topic { get; set; }

        [Required]
        [StringLength(100)]
        public string URL { get; set; }

        [Required]
        [StringLength(50)]
        public string DisplayText { get; set; }

        public int? UserID { get; set; }

        public virtual BTTUser BTTUser { get; set; }
    }
}
