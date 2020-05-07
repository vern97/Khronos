namespace BeyondTheTutor.Models.TimeSheetModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class TimeSheet
    {
        public Dictionary<byte, string> getMonths()
        {
            Dictionary<byte, string> d_months = new Dictionary<byte, string>()
            {
                { 1, "JAN" },
                { 2, "JAN - FEB" },
                { 3, "FEB" },
                { 4, "FEB - MAR" },
                { 5, "MAR" },
                { 6, "MAR - APR" },
                { 7, "APR" },
                { 8, "APR - MAY" },
                { 9, "MAY" },
                { 10, "MAY - JUN" },
                { 11, "JUN" },
                { 12, "JUN - JUL" },
                { 13, "JUL" },
                { 14, "JUL - AUG" },
                { 15, "AUG" },
                { 16, "AUG - SEP" },
                { 17, "SEP" },
                { 18, "SEP - OCT" },
                { 19, "OCT" },
                { 20, "OCT - NOV" },
                { 21, "NOV" },
                { 22, "NOV - DEC" },
                { 23, "DEC" },
                { 24, "DEC - JAN" }
            };

            return d_months;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TimeSheet()
        {
            Days = new HashSet<Day>();
        }

        public int ID { get; set; }

        public byte Month { get; set; }

        public short Year { get; set; }

        public int TutorID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Day> Days { get; set; }

        public virtual Tutor Tutor { get; set; }
    }
}
