namespace BeyondTheTutor.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Professor
    {
        public int ID { get; set; }

        public bool AdminApproved { get; set; }

        public virtual BTTUser BTTUser { get; set; }
    }
}
