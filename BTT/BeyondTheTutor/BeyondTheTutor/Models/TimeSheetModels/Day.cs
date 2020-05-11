namespace BeyondTheTutor.Models.TimeSheetModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Day
    {
        public Dictionary<byte, string> getDays()
        {
            Dictionary<byte, string> d_days = new Dictionary<byte, string>();

            for (byte i = 10; i < 31; i++)
            {
                byte c = (byte)(i + 1);
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

            for (byte i = 0; i < 10; i++)
            {
                byte c = (byte)(i + 1);
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

        public int RegularHrs { get; set; }

        public double getPayRollTime(int mins)
        {
            var hours = mins/60;

            var minutes = mins % 60;
                

            switch (
                minutes >= 57 ? 1.0 :
                minutes >= 51 ? .9 :
                minutes >= 45 ? .8 :
                minutes >= 39 ? .7 :
                minutes >= 33 ? .6 :
                minutes >= 27 ? .5 :
                minutes >= 21 ? .4 :
                minutes >= 15 ? .3 :
                minutes >= 9 ? .2 :
                minutes >= 3 ? .1 : .0)
            {
                case 1:
                    return hours + 1.0;
                case .9:
                    return hours + .9;
                case .8:
                    return hours + .8;
                case .7:
                    return hours + .7;
                case .6:
                    return hours + .6;
                case .5:
                    return hours + .5;
                case .4:
                    return hours + .4;
                case .3:
                    return hours + .3;
                case .2:
                    return hours + .2;
                case .1:
                    return hours + .1;
                case .0:
                    return hours;
                default:
                    return hours;
            }
        }

        public int TimeSheetID { get; set; }

        public virtual TimeSheet TimeSheet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkHour> WorkHours { get; set; }
    }
}
