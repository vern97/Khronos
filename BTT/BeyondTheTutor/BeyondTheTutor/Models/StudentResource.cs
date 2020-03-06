namespace BeyondTheTutor.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class StudentResource
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Topic { get; set; }

        [Required]
        [StringLength(100)]
        public string URL { get; set; }

        [Required]
        [StringLength(50)]
        public string DisplayText { get; set; }

        public int? UserID { get; set; }

        public virtual BTTUser BTTUser { get; set; }
    }
}
