namespace BeyondTheTutor.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tutor
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public Int16 ClassOf { get; set; }

        [Required]
        [StringLength(9)]
        public string VNumber { get; set; }

        [Required]
        [StringLength(128)]
        public string ASPNetIdentityID { get; set; }
    }
}
