using DIMS_Core.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DIMS_Core.DataAccessLayer.Context
{
    public partial class DIMSCoreDatabaseContext : DbContext
    {
        public DIMSCoreDatabaseContext()
        {
        }

        public DIMSCoreDatabaseContext(DbContextOptions<DIMSCoreDatabaseContext> options)
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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=DIMS-Core.Database; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Direction>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ_Direction_Name")
                    .IsUnique();

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ_Task_Name")
                    .IsUnique();

                entity.Property(e => e.DeadlineDate).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.StartDate).HasColumnType("date");
            });

            modelBuilder.Entity<TaskState>(entity =>
            {
                entity.HasKey(e => e.StateId)
                    .HasName("PK_TaskState_StateId");

                entity.HasIndex(e => e.StateName)
                    .HasName("UQ_TaskState_StateName")
                    .IsUnique();

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TaskTrack>(entity =>
            {
                entity.Property(e => e.TrackDate).HasColumnType("date");

                entity.Property(e => e.TrackNote).HasMaxLength(100);

                entity.HasOne(d => d.UserTask)
                    .WithMany(p => p.TaskTrack)
                    .HasForeignKey(d => d.UserTaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaskTrack_To_UserTask");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_UserProfile_UserId");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ_UserProfile_Email")
                    .IsUnique();

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Education)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('Not indicated')");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MobilePhone)
                    .HasMaxLength(25)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Sex)
                    .HasMaxLength(25)
                    .IsFixedLength();

                entity.Property(e => e.Skype).HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Direction)
                    .WithMany(p => p.UserProfile)
                    .HasForeignKey(d => d.DirectionId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_UserProfile_To_Direction");
            });

            modelBuilder.Entity<UserTask>(entity =>
            {
                entity.Property(e => e.StateId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.UserTask)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserTask_To_TaskState");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.UserTask)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_UserTask_To_Task");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTask)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserTask_To_UserProfile");
            });

            modelBuilder.Entity<VTask>(entity =>
            {
                entity.HasKey(e => e.TaskId);

                entity.ToView("vTask");

                entity.Property(e => e.DeadlineDate).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.TaskId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<VUserProfile>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToView("vUserProfile");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Direction)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsFixedLength();

                entity.Property(e => e.Education).HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(101);

                entity.Property(e => e.MobilePhone)
                    .HasMaxLength(25)
                    .IsFixedLength();

                entity.Property(e => e.Sex)
                    .HasMaxLength(25)
                    .IsFixedLength();

                entity.Property(e => e.Skype).HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("date");
            });

            modelBuilder.Entity<VUserProgress>(entity =>
            {
                entity.HasKey(e => e.TaskTrackId);

                entity.ToView("vUserProgress");

                entity.Property(e => e.TaskName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.TrackDate).HasColumnType("date");

                entity.Property(e => e.TrackNote).HasMaxLength(100);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(101);
            });

            modelBuilder.Entity<VUserTask>(entity =>
            {
                entity.HasKey(e => e.TaskId);

                entity.ToView("vUserTask");

                entity.Property(e => e.DeadlineDate).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.TaskName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsFixedLength();
            });

            modelBuilder.Entity<VUserTrack>(entity =>
            {
                entity.HasKey(e => e.TaskTrackId);

                entity.ToView("vUserTrack");

                entity.Property(e => e.TaskName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.TrackDate).HasColumnType("date");

                entity.Property(e => e.TrackNote).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}