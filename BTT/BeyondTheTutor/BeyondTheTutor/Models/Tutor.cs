namespace BeyondTheTutor.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tutor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tutor()
        {
            TutoringAppts = new HashSet<TutoringAppt>();
            TutoringServiceAlerts = new HashSet<TutoringServiceAlert>();
            TutorSchedules = new HashSet<TutorSchedule>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public short ClassOf { get; set; }

        [Required]
        [StringLength(9)]
        public string VNumber { get; set; }

        public bool AdminApproved { get; set; } = false;

        public virtual BTTUser BTTUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TutoringAppt> TutoringAppts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TutoringServiceAlert> TutoringServiceAlerts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TutorSchedule> TutorSchedules { get; set; }
    }
}