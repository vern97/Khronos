namespace BeyondTheTutor.Models.ProfilePictureModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProfilePicture
    {
        public int ID { get; set; }

        public byte[] ImagePath { get; set; }

        public int UserID { get; set; }

        public virtual BTTUser BTTUser { get; set; }
    }
}
