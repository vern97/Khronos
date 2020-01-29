namespace SwimMeetTracker.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using SwimMeetTracker.Models;

    public partial class TrackMeetContext : DbContext
    {
        public TrackMeetContext()
            : base("name=TrackMeetContext")
        {
        }

        public virtual DbSet<Athlete> Athletes { get; set; }
        public virtual DbSet<AthleteTeam> AthleteTeams { get; set; }
        public virtual DbSet<Coach> Coaches { get; set; }
        public virtual DbSet<Meeting> Meetings { get; set; }
        public virtual DbSet<Race> Races { get; set; }
        public virtual DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Athlete>()
                .HasMany(e => e.AthleteTeams)
                .WithRequired(e => e.Athlete)
                .HasForeignKey(e => e.AthleteID);

            modelBuilder.Entity<Athlete>()
                .HasMany(e => e.Races)
                .WithRequired(e => e.Athlete)
                .HasForeignKey(e => e.AthleteID);

            modelBuilder.Entity<Coach>()
                .HasMany(e => e.Teams)
                .WithRequired(e => e.Coach)
                .HasForeignKey(e => e.CoachID);

            modelBuilder.Entity<Meeting>()
                .HasMany(e => e.Races)
                .WithRequired(e => e.Meeting)
                .HasForeignKey(e => e.MeetingID);

            modelBuilder.Entity<Team>()
                .HasMany(e => e.AthleteTeams)
                .WithRequired(e => e.Team)
                .HasForeignKey(e => e.TeamID);
        }
    }
}
