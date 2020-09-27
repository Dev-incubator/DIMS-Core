using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DIMS_Core.Models
{
    public partial class DIMSContext : DbContext
    {
        public DIMSContext()
        {
        }

        public DIMSContext(DbContextOptions<DIMSContext> options)
            : base(options)
        {
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
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-N10FT1S;Database=DIMS;Trusted_Connection=True;");
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
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TaskTrack__UserT__2E1BDC42");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserProf__1788CC4C72B70504");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.BirthOfDate).HasColumnType("datetime");

                entity.Property(e => e.Education)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.MobilePhone)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Skype)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Direction)
                    .WithMany(p => p.UserProfile)
                    .HasForeignKey(d => d.DirectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserProfi__Direc__2F10007B");
            });

            modelBuilder.Entity<UserTask>(entity =>
            {
                entity.HasOne(d => d.State)
                    .WithMany(p => p.UserTask)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserTask__StateI__31EC6D26");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.UserTask)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserTask__TaskId__300424B4");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTask)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserTask__UserId__30F848ED");
            });

            modelBuilder.Entity<VTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vTask");

                entity.Property(e => e.DeadlineDate).HasMaxLength(4000);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.StartDate).HasMaxLength(4000);

                entity.Property(e => e.TaskId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<VUserProfile>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vUserProfile");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Direction)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Education)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(511);

                entity.Property(e => e.MobilePhone)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Skype)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UserProfile).HasMaxLength(4000);
            });

            modelBuilder.Entity<VUserProgress>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vUserProgress");

                entity.Property(e => e.TaskName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.TrackDate).HasMaxLength(4000);

                entity.Property(e => e.TrackNote).HasMaxLength(255);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(511);
            });

            modelBuilder.Entity<VUserTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vUserTask");

                entity.Property(e => e.DeadlineDate).HasMaxLength(4000);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.StartDate).HasMaxLength(4000);

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

                entity.Property(e => e.TrackDate).HasMaxLength(4000);

                entity.Property(e => e.TrackNote).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
