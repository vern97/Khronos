namespace BeyondTheTutor.DAL
{
    
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using BeyondTheTutor.Models;


    public partial class BeyondTheTutorContext : DbContext
    {
        public BeyondTheTutorContext()
            : base("name=BeyondTheTutorContext")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Professor> Professors { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Tutor> Tutors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
