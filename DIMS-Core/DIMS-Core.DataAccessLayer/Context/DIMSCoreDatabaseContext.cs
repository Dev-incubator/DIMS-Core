using System;
using DIMS_Core.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace DIMS_Core.DataAccessLayer.Context
{
    public partial class DIMSCoreDatabaseContext : DbContext
    {
        private readonly IConfiguration configuration;

        public DIMSCoreDatabaseContext() { }

        public DIMSCoreDatabaseContext(DbContextOptions<DIMSCoreDatabaseContext> options) : base(options) { }

        public DIMSCoreDatabaseContext(DbContextOptions<DIMSCoreDatabaseContext> options, IConfiguration configuration)
            : base(options) 
        {
            this.configuration = configuration;
        }

        public virtual DbSet<Direction> Direction { get; set; }
        public virtual DbSet<Task> Task { get; set; }
        public virtual DbSet<TaskState> TaskState { get; set; }
        public virtual DbSet<TaskTrack> TaskTrack { get; set; }
        public virtual DbSet<UserProfile> UserProfile { get; set; }
        public virtual DbSet<UserTask> UserTask { get; set; }
        public virtual DbSet<VTask> VTask { get; set; }
        public virtual DbSet<VUserProfile> VUserProfile { get; set; }
        public virtual DbSet<VUserProgress> VUserProgress { get; set; }
        public virtual DbSet<VUserTask> VUserTask { get; set; }
        public virtual DbSet<VUserTrack> VUserTrack { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DIMSDatabase"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Direction>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.Property(e => e.DeadlineDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TaskState>(entity =>
            {
                entity.HasKey(e => e.StateId)
                    .HasName("PK__TaskStat__C3BA3B3A00AB31A3");

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<TaskTrack>(entity =>
            {
                entity.Property(e => e.TrackDate).HasColumnType("datetime");

                entity.Property(e => e.TrackNote).HasMaxLength(255);

                entity.HasOne(d => d.UserTask)
                    .WithMany(p => p.TaskTrack)
                    .HasForeignKey(d => d.UserTaskId)
                    .HasConstraintName("FK__TaskTrack__UserT__3A81B327");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserProf__1788CC4C72B70504");

                entity.Property(e => e.Address).HasMaxLength(128);

                entity.Property(e => e.BirthOfDate).HasColumnType("datetime");

                entity.Property(e => e.Education).HasMaxLength(128);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.MobilePhone).HasMaxLength(16);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Skype).HasMaxLength(255);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Direction)
                    .WithMany(p => p.UserProfile)
                    .HasForeignKey(d => d.DirectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserProfi__Direc__2F10007B");
            });

            modelBuilder.Entity<UserTask>(entity =>
            {
                entity.HasKey(e => e.UserTaskId);

                entity.HasOne(d => d.State)
                    .WithMany(p => p.UserTask)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK__UserTask__StateI__3D5E1FD2");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.UserTask)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK__UserTask__TaskId__3B75D760");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTask)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserTask__UserId__3C69FB99");
            });

            modelBuilder.Entity<VTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vTask");

                entity.Property(e => e.DeadlineDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.TaskId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<VUserProfile>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToView("vUserProfile");

                entity.Property(e => e.Address).HasMaxLength(128);

                entity.Property(e => e.Direction)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Education).HasMaxLength(128);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(129);

                entity.Property(e => e.MobilePhone).HasMaxLength(16);

                entity.Property(e => e.Skype).HasMaxLength(255);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<VUserProgress>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vUserProgress");

                entity.Property(e => e.TaskName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.TrackDate).HasColumnType("datetime");

                entity.Property(e => e.TrackNote).HasMaxLength(255);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(129);
            });

            modelBuilder.Entity<VUserTask>(entity =>
            {
                //Inmemory tests don't work without primary key.
                entity.HasKey(e => e.TestOnlyKey);

                entity.ToView("vUserTask");

                entity.Property(e => e.DeadlineDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.TaskName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<VUserTrack>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vUserTrack");

                entity.Property(e => e.TaskName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.TrackDate).HasColumnType("datetime");

                entity.Property(e => e.TrackNote).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
