namespace BeyondTheTutor.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Admin
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public virtual BTTUser BTTUser { get; set; }

    }
}
