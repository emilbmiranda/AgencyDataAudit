using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AgencyDataAuditAPI.Models;

namespace AgencyDataAuditAPI.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ConsumerReportingAgency> ConsumerReportingAgencies { get; set; }

    public virtual DbSet<ErrorCategory> ErrorCategories { get; set; }

    public virtual DbSet<FileState> FileStates { get; set; }

    public virtual DbSet<FileType> FileTypes { get; set; }

    public virtual DbSet<Metro2> Metro2s { get; set; }

    public virtual DbSet<RejectCategory> RejectCategories { get; set; }

    public virtual DbSet<ReportingPeriod> ReportingPeriods { get; set; }

    public virtual DbSet<ReportingPeriodFile> ReportingPeriodFiles { get; set; }

    public virtual DbSet<Statistic> Statistics { get; set; }

    public virtual DbSet<SystemOfRecord> SystemOfRecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;User ID=sa;Password=reallyStrongPwd123;Pooling=False;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Authentication=SqlPassword;Application Name=vscode-mssql;Application Intent=ReadWrite;Command Timeout=30");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConsumerReportingAgency>(entity =>
        {
            entity.HasKey(e => e.ConsumerReportingAgencyIdId);

            entity.ToTable("ConsumerReportingAgency");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ErrorCategory>(entity =>
        {
            entity.HasKey(e => e.ErrorCategoryId).HasName("PK_ErrorLookup");

            entity.ToTable("ErrorCategory");

            entity.Property(e => e.ErrorCategory1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ErrorCategory");
        });

        modelBuilder.Entity<FileState>(entity =>
        {
            entity.ToTable("FileState");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FileType>(entity =>
        {
            entity.HasKey(e => e.FileTypeId).HasName("PK_FileTypeId");

            entity.ToTable("FileType");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Metro2>(entity =>
        {
            entity.ToTable("Metro2");

            entity.Property(e => e.AccountNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.ReportingPeriodFile).WithMany(p => p.Metro2s)
                .HasForeignKey(d => d.ReportingPeriodFileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Metro2_ReportingPeriodFile");
        });

        modelBuilder.Entity<RejectCategory>(entity =>
        {
            entity.ToTable("RejectCategory");

            entity.Property(e => e.Name).IsUnicode(false);

            entity.HasOne(d => d.ErrorCategory).WithMany(p => p.RejectCategories)
                .HasForeignKey(d => d.ErrorCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RejectCategory_ErrorCategory");
        });

        modelBuilder.Entity<ReportingPeriod>(entity =>
        {
            entity.ToTable("ReportingPeriod");

            entity.Property(e => e.ActivityDate).HasColumnType("datetime");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DEFAULT_ReportingPeriod_IsActive");

            entity.HasOne(d => d.SystemOfRecord).WithMany(p => p.ReportingPeriods)
                .HasForeignKey(d => d.SystemOfRecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReportingPeriod_SystemOfRecord");
        });

        modelBuilder.Entity<ReportingPeriodFile>(entity =>
        {
            entity.ToTable("ReportingPeriodFile");

            entity.Property(e => e.ReportingPeriodFileId).ValueGeneratedOnAdd();
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Filename).IsUnicode(false);

            entity.HasOne(d => d.ConsumerReportingAgency).WithMany(p => p.ReportingPeriodFiles)
                .HasForeignKey(d => d.ConsumerReportingAgencyId)
                .HasConstraintName("FK_ReportingPeriodFile_ConsumerReportingAgency");

            entity.HasOne(d => d.FileState).WithMany(p => p.ReportingPeriodFiles)
                .HasForeignKey(d => d.FileStateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReportingPeriodFile_FileState");

            entity.HasOne(d => d.FileType).WithMany(p => p.ReportingPeriodFiles)
                .HasForeignKey(d => d.FileTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReportingPeriodFile_FileType");

            entity.HasOne(d => d.ReportingPeriodFileNavigation).WithOne(p => p.ReportingPeriodFile)
                .HasForeignKey<ReportingPeriodFile>(d => d.ReportingPeriodFileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReportingPeriodFile_ReportingPeriod");
        });

        modelBuilder.Entity<Statistic>(entity =>
        {
            entity.ToTable("Statistic");

            entity.HasOne(d => d.ReportingPeriodFile).WithMany(p => p.Statistics)
                .HasForeignKey(d => d.ReportingPeriodFileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Statistic_ReportingPeriodFile");
        });

        modelBuilder.Entity<SystemOfRecord>(entity =>
        {
            entity.ToTable("SystemOfRecord");

            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
