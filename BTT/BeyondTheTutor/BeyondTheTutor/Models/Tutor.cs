namespace BeyondTheTutor.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using BeyondTheTutor.Models.TimeSheetModels;


    public partial class Tutor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tutor()
        {
            TutoringAppts = new HashSet<TutoringAppt>();
            TutoringServiceAlerts = new HashSet<TutoringServiceAlert>();
            TutorSchedules = new HashSet<TutorSchedule>();
            TimeSheets = new HashSet<TimeSheet>();

        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public short ClassOf { get; set; }

        [Required]
        [StringLength(9)]
        //CUSTOM CHANGE PLEASE SAVE
        [RegularExpression(@"^[vV][0-9]{8}$", ErrorMessage = "Your V number seems to be broken.")]
        public string VNumber { get; set; }

        public bool AdminApproved { get; set; } = false;

        public virtual BTTUser BTTUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TutoringAppt> TutoringAppts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TutoringServiceAlert> TutoringServiceAlerts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TutorSchedule> TutorSchedules { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeSheet> TimeSheets { get; set; }
    }
}
