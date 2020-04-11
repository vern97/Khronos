namespace BeyondTheTutor.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using BeyondTheTutor.Models;
    using BeyondTheTutor.Models.SurveyModels;
    using BeyondTheTutor.DAL;
    public partial class BeyondTheTutorContext : DbContext
    {
        public BeyondTheTutorContext()
             : base(new ContextGetter().getContext)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<BTTUser> BTTUsers { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Professor> Professors { get; set; }
        public virtual DbSet<StudentResource> StudentResources { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<TutoringAppt> TutoringAppts { get; set; }
        public virtual DbSet<TutoringServiceAlert> TutoringServiceAlerts { get; set; }
        public virtual DbSet<Tutor> Tutors { get; set; }
        public virtual DbSet<TutorSchedule> TutorSchedules { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Survey> Surveys { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BTTUser>()
                .HasOptional(e => e.Admin)
                .WithRequired(e => e.BTTUser)
                .WillCascadeOnDelete();

            modelBuilder.Entity<BTTUser>()
                .HasOptional(e => e.Professor)
                .WithRequired(e => e.BTTUser)
                .WillCascadeOnDelete();

            modelBuilder.Entity<BTTUser>()
                .HasOptional(e => e.Student)
                .WithRequired(e => e.BTTUser)
                .WillCascadeOnDelete();

            modelBuilder.Entity<BTTUser>()
                .HasOptional(e => e.Tutor)
                .WithRequired(e => e.BTTUser)
                .WillCascadeOnDelete();

            modelBuilder.Entity<BTTUser>()
                .HasMany(e => e.StudentResources)
                .WithOptional(e => e.BTTUser)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<TutoringAppt>()
                .Property(e => e.Note)
                .IsUnicode(false);
            
            modelBuilder.Entity<Question>()
                .Property(e => e.AskingQuestion)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.Answers)
                .WithRequired(e => e.Question)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Survey>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Survey>()
                .HasMany(e => e.Answers)
                .WithRequired(e => e.Survey)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Survey>()
                .HasMany(e => e.Questions)
                .WithRequired(e => e.Survey)
                .WillCascadeOnDelete();
        }

    }
}
