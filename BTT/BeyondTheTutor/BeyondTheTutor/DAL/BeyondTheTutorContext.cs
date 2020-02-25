namespace BeyondTheTutor.DAL
{
    using System.Data.Entity;
    using BeyondTheTutor.Models;

    public partial class BeyondTheTutorContext : DbContext
    {
        public BeyondTheTutorContext()
            : base("name=BTTContext_Azure")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<BTTUser> BTTUsers { get; set; }
        public virtual DbSet<Professor> Professors { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Tutor> Tutors { get; set; }
        public virtual DbSet<TutorSchedule> TutorSchedules { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BTTUser>()
                .HasOptional(e => e.Admin)
                .WithRequired(e => e.BTTUser);

            modelBuilder.Entity<BTTUser>()
                .HasOptional(e => e.Professor)
                .WithRequired(e => e.BTTUser);

            modelBuilder.Entity<BTTUser>()
                .HasOptional(e => e.Student)
                .WithRequired(e => e.BTTUser);

            modelBuilder.Entity<BTTUser>()
                .HasOptional(e => e.Tutor)
                .WithRequired(e => e.BTTUser);
        }
    }
}
