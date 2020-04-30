namespace BeyondTheTutor.Models.TimeSheetModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Day
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Day()
        {
            WorkHours = new HashSet<WorkHour>();
        }

        public int ID { get; set; }

        public byte On { get; set; }

        public decimal? RegularHrs { get; set; }

        public int TimeSheetID { get; set; }

        public virtual TimeSheet TimeSheet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkHour> WorkHours { get; set; }
    }
}
