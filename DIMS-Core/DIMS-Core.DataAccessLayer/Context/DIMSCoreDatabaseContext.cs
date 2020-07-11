﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DIMS_Core.DataAccessLayer.Entities;

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

        public virtual DbSet<Direction> Directions { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskState> TaskStates { get; set; }
        public virtual DbSet<TaskTrack> TaskTracks { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<UserTask> UserTasks { get; set; }
        public virtual DbSet<VTask> VTask { get; set; }
        public virtual DbSet<VUserProfile> VUserProfile { get; set; }
        public virtual DbSet<VUserProgress> VUserProgress { get; set; }
        public virtual DbSet<VUserTask> VUserTask { get; set; }
        public virtual DbSet<VUserTrack> VUserTrack { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DIMS-Core.Database;Trusted_Connection=True;");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Direction>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ_Direction_Name")
                    .IsUnique();

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .HasMaxLength(25)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ_Task_Name")
                    .IsUnique();

                entity.Property(e => e.TaskId).ValueGeneratedNever();

                entity.Property(e => e.DeadlineDate).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .HasMaxLength(25)
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

                entity.Property(e => e.StateId).ValueGeneratedNever();

                entity.Property(e => e.StateName)
                    .HasMaxLength(25)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TaskTrack>(entity =>
            {
                entity.Property(e => e.TaskTrackId).ValueGeneratedNever();

                entity.Property(e => e.TrackDate).HasColumnType("date");

                entity.Property(e => e.TrackNote).HasMaxLength(50);

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

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MobilePhone)
                    .HasMaxLength(25)
                    .IsFixedLength();

                entity.Property(e => e.Name).HasMaxLength(50);

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
                entity.Property(e => e.UserTaskId).ValueGeneratedNever();

                entity.HasOne(d => d.State)
                    .WithMany(p => p.UserTask)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.SetNull)
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
                entity.HasNoKey();

                entity.ToView("vTask");

                entity.Property(e => e.DeadlineDate).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .HasMaxLength(25)
                    .IsFixedLength();

                entity.Property(e => e.StartDate).HasColumnType("date");
            });

            modelBuilder.Entity<VUserProfile>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vUserProfile");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Direction)
                    .HasMaxLength(25)
                    .IsFixedLength();

                entity.Property(e => e.Education).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(101);

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
                entity.HasNoKey();

                entity.ToView("vUserProgress");

                entity.Property(e => e.TaskName)
                    .HasMaxLength(25)
                    .IsFixedLength();

                entity.Property(e => e.TrackDate).HasColumnType("date");

                entity.Property(e => e.TrackNote).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(101);
            });

            modelBuilder.Entity<VUserTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vUserTask");

                entity.Property(e => e.DeadlineDate).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.State)
                    .HasMaxLength(25)
                    .IsFixedLength();

                entity.Property(e => e.TaskName)
                    .HasMaxLength(25)
                    .IsFixedLength();
            });

            modelBuilder.Entity<VUserTrack>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vUserTrack");

                entity.Property(e => e.TaskName)
                    .HasMaxLength(25)
                    .IsFixedLength();

                entity.Property(e => e.TrackDate).HasColumnType("date");

                entity.Property(e => e.TrackNote).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
