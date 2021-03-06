namespace BeyondTheTutor.Models.SMSModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SMSArchive
    {
        public int ID { get; set; }

        public DateTime DateSent { get; set; }

        [Required]
        [StringLength(100)]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }

        public int Sender { get; set; }

        public int Receiver { get; set; }

        public int Priority { get; set; }

        public virtual BTTUser BTTUser { get; set; }
    }
}
