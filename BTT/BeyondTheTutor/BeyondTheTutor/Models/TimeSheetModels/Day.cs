namespace BeyondTheTutor.Models.TimeSheetModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Day
    {
        public Dictionary<int, string> getDays()
        {
            Dictionary<int, string> d_days = new Dictionary<int, string>();

            for (int i = 10; i < 31; i++)
            {
                int c = i + 1;
                string val = "";

                switch (c)
                {
                    case 21: case 31:
                        val = c.ToString() + "st";
                        d_days.Add(c, val);
                        break;
                    case 22:
                        val = c.ToString() + "nd";
                        d_days.Add(c, val);
                        break;
                    case 23:
                        val = c.ToString() + "rd";
                        d_days.Add(c, val);
                        break;
                    default:
                        val = c.ToString() + "th";
                        d_days.Add(c, val);
                        break;
                }
            }

            for (int i = 0; i < 10; i++)
            {
                int c = i + 1;
                string val = "";

                switch (c)
                {
                    case 1:
                        val = c.ToString() + "st";
                        d_days.Add(c, val);
                        break;
                    case 2:
                        val = c.ToString() + "nd";
                        d_days.Add(c, val);
                        break;
                    case 3:
                        val = c.ToString() + "rd";
                        d_days.Add(c, val);
                        break;
                    default:
                        val = c.ToString() + "th";
                        d_days.Add(c, val);
                        break;
                }
            }

            return d_days;
        }

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
