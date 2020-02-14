namespace BeyondTheTutor.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    

    public partial class Student
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public DateTime GraduatingYear { get; set; }

        [Required]
        [StringLength(1)]
        public string ClassStanding { get; set; }

        [Required]
        [StringLength(128)]
        public string ASPNetIdentityID { get; set; }
    }
}
