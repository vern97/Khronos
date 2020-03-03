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
             //: base("name=BTTContext_Azure")
             : base("name=BeyondTheTutorContext")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<BTTUser> BTTUsers { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Professor> Professors { get; set; }
        public virtual DbSet<StudentResource> StudentResources { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<TutoringAppt> TutoringAppts { get; set; }
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

            modelBuilder.Entity<BTTUser>()
                .HasMany(e => e.StudentResources)
                .WithOptional(e => e.BTTUser)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<TutoringAppt>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<Tutor>()
                .HasMany(e => e.TutoringAppts)
                .WithOptional(e => e.Tutor)
                .WillCascadeOnDelete();
        }
    }
}
