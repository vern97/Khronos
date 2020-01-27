namespace SwimMeetTracker.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Meeting
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Meeting()
        {
            Races = new HashSet<Race>();
        }

        [Key]
        public int EID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public DateTime MeetingDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Race> Races { get; set; }
    }
}
