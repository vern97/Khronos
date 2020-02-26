namespace BeyondTheTutor.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BTTUser
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(128)]
        public string ASPNetIdentityID { get; set; }

        public virtual Admin Admin { get; set; }

        public virtual Professor Professor { get; set; }

        public virtual Student Student { get; set; }

        public virtual Tutor Tutor { get; set; }
    }
}
