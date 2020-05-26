namespace BeyondTheTutor.Models.SMSModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SMS")]
    public partial class SM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SM()
        {
            SMSReplies = new HashSet<SMSReply>();
            SMSStatuses = new HashSet<SMSStatus>();
        }

        public int ID { get; set; }

        public DateTime DateSent { get; set; }

        [StringLength(100)]
        public string Subject { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Message { get; set; }

        public int Sender { get; set; }

        public int? Receiver { get; set; }

        public int Priority { get; set; }

        public virtual BTTUser BTTUser { get; set; }

        public virtual BTTUser BTTUser1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SMSReply> SMSReplies { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SMSStatus> SMSStatuses { get; set; }
    }
}
