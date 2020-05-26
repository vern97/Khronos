namespace BeyondTheTutor.Models.SMSModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SMSStatuses")]
    public partial class SMSStatus
    {
        public int ID { get; set; }

        public bool? Sent { get; set; }

        public bool? Received { get; set; }

        public bool? Read { get; set; }

        public bool? Saved { get; set; }

        public int SMSID { get; set; }

        public virtual SM SM { get; set; }
    }
}
