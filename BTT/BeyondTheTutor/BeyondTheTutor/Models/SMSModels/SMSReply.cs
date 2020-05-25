namespace BeyondTheTutor.Models.SMSModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SMSReply
    {
        public int ID { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Response { get; set; }

        public int Sender { get; set; }

        public int Receiver { get; set; }

        public int SMSID { get; set; }

        public virtual BTTUser BTTUser { get; set; }

        public virtual BTTUser BTTUser1 { get; set; }

        public virtual SM SM { get; set; }
    }
}
