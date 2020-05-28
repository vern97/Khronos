namespace BeyondTheTutor.DAL
{
    using System.Data.Entity;
    using BeyondTheTutor.Models;
    using BeyondTheTutor.Models.SurveyModels;
    using BeyondTheTutor.Models.TimeSheetModels;
	using BeyondTheTutor.Models.ProfilePictureModels;
    using BeyondTheTutor.Models.SMSModels;
    using BeyondTheTutor.Models.StudentAlertModels;
	
    public partial class BeyondTheTutorContext : DbContext
    {
        public BeyondTheTutorContext()
             : base(new ContextGetter().getContext)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<BTTUser> BTTUsers { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<CumulativeGPA> CumulativeGPAs { get; set; }
        public virtual DbSet<FinalGrade> FinalGrades { get; set; }
        public virtual DbSet<Professor> Professors { get; set; }
        public virtual DbSet<ProfilePicture> ProfilePictures { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<StudentResource> StudentResources { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Survey> Surveys { get; set; }
        public virtual DbSet<TutoringAppt> TutoringAppts { get; set; }
        public virtual DbSet<TutoringServiceAlert> TutoringServiceAlerts { get; set; }
        public virtual DbSet<Tutor> Tutors { get; set; }
        public virtual DbSet<TutorSchedule> TutorSchedules { get; set; }
        public virtual DbSet<WeightedGrade> WeightedGrades { get; set; }
        public virtual DbSet<Day> Days { get; set; }
        public virtual DbSet<TimeSheet> TimeSheets { get; set; }
        public virtual DbSet<WorkHour> WorkHours { get; set; }
        public virtual DbSet<SM> SMS { get; set; }
        public virtual DbSet<SMSArchive> SMSArchives { get; set; }
        public virtual DbSet<SMSReply> SMSReplies { get; set; }
        public virtual DbSet<SMSStatus> SMSStatuses { get; set; }
        public virtual DbSet<StudentAlert> StudentAlerts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Day>()
                .Property(e => e.RegularHrs);

            modelBuilder.Entity<Day>()
                .HasMany(e => e.WorkHours)
                .WithRequired(e => e.Day)
                .HasForeignKey(e => e.DayID);

            modelBuilder.Entity<TimeSheet>()
                .Property(e => e.Month);

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
                .HasMany(e => e.CumulativeGPAs)
                .WithRequired(e => e.BTTUser)
                .HasForeignKey(e => e.UserID);

            modelBuilder.Entity<BTTUser>()
                .HasMany(e => e.FinalGrades)
                .WithRequired(e => e.BTTUser)
                .HasForeignKey(e => e.UserID);

            modelBuilder.Entity<BTTUser>()
                .HasMany(e => e.StudentResources)
                .WithOptional(e => e.BTTUser)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<BTTUser>()
                .HasMany(e => e.WeightedGrades)
                .WithRequired(e => e.BTTUser)
                .HasForeignKey(e => e.UserID);

            modelBuilder.Entity<TutoringAppt>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<BTTUser>()
                .HasMany(e => e.Answers)
                .WithRequired(e => e.BTTUser)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Question>()
                .Property(e => e.AskingQuestion)
                .IsUnicode(false);

            modelBuilder.Entity<Survey>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Survey>()
                .HasMany(e => e.Answers)
                .WithRequired(e => e.Survey)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BTTUser>()
                .HasOptional(e => e.Tutor)
                .WithRequired(e => e.BTTUser)
                .WillCascadeOnDelete();

            modelBuilder.Entity<BTTUser>()
                .HasMany(e => e.ProfilePictures)
                .WithRequired(e => e.BTTUser)
                .HasForeignKey(e => e.UserID);

            modelBuilder.Entity<BTTUser>()
                .HasMany(e => e.SMS)
                .WithOptional(e => e.BTTUser)
                .HasForeignKey(e => e.Receiver);

            modelBuilder.Entity<BTTUser>()
                .HasMany(e => e.SMS1)
                .WithRequired(e => e.BTTUser1)
                .HasForeignKey(e => e.Sender);

            modelBuilder.Entity<BTTUser>()
                .HasMany(e => e.SMSArchives)
                .WithOptional(e => e.BTTUser)
                .HasForeignKey(e => e.Receiver)
                .WillCascadeOnDelete();

            modelBuilder.Entity<BTTUser>()
                .HasMany(e => e.SMSReplies)
                .WithRequired(e => e.BTTUser)
                .HasForeignKey(e => e.Receiver)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BTTUser>()
                .HasMany(e => e.SMSReplies1)
                .WithRequired(e => e.BTTUser1)
                .HasForeignKey(e => e.Sender);

            modelBuilder.Entity<BTTUser>()
                .HasMany(e => e.StudentAlerts)
                .WithRequired(e => e.BTTUser)
                .HasForeignKey(e => e.AdminID);

            modelBuilder.Entity<SM>()
                .Property(e => e.Subject)
                .IsUnicode(false);

            modelBuilder.Entity<SM>()
                .Property(e => e.Message)
                .IsUnicode(false);

            modelBuilder.Entity<SM>()
                .HasMany(e => e.SMSReplies)
                .WithRequired(e => e.SM)
                .HasForeignKey(e => e.SMSID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SM>()
                .HasMany(e => e.SMSStatuses)
                .WithRequired(e => e.SM)
                .HasForeignKey(e => e.SMSID);

            modelBuilder.Entity<SMSArchive>()
                .Property(e => e.Subject)
                .IsUnicode(false);

            modelBuilder.Entity<SMSArchive>()
                .Property(e => e.Message)
                .IsUnicode(false);

            modelBuilder.Entity<SMSReply>()
                .Property(e => e.Response)
                .IsUnicode(false);

            modelBuilder.Entity<StudentAlert>()
                .Property(e => e.Message)
                .IsUnicode(false);
        }
    }
}
