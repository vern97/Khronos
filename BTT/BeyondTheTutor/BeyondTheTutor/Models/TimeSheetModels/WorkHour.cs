namespace BeyondTheTutor.Models.TimeSheetModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WorkHour
    {
        public int ID { get; set; }

        public DateTime ClockedIn { get; set; }

        public DateTime ClockedOut { get; set; }

        public int TimeSheetID { get; set; }

        public virtual Day Day { get; set; }
    }
}
