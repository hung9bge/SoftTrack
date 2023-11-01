using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace SoftTrack.Domain
{
    public partial class soft_track3Context : DbContext
    {
        public soft_track3Context()
        {
        }

        public soft_track3Context(DbContextOptions<soft_track3Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Authorization> Authorizations { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<DeviceSoftware> DeviceSoftwares { get; set; }
        public virtual DbSet<License> Licenses { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Software> Softwares { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(config.GetConnectionString("MyConnectionString"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccId);

                entity.ToTable("Account");

                entity.Property(e => e.AccId).HasColumnName("AccID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Role");
            });

            modelBuilder.Entity<Authorization>(entity =>
            {
                entity.ToTable("Authorization");

                entity.Property(e => e.AuthorizationId).HasColumnName("AuthorizationID");

                entity.Property(e => e.AuthorizationKey)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Publisher)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.SoftwareId).HasColumnName("SoftwareID");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("Start_Date");

                entity.HasOne(d => d.Software)
                    .WithMany(p => p.Authorizations)
                    .HasForeignKey(d => d.SoftwareId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Authorization_Software");
            });

            modelBuilder.Entity<Device>(entity =>
            {
                entity.ToTable("Device");

                entity.Property(e => e.DeviceId).HasColumnName("DeviceID");

                entity.Property(e => e.Cpu)
                    .HasMaxLength(255)
                    .HasColumnName("CPU");

                entity.Property(e => e.Gpu)
                    .HasMaxLength(255)
                    .HasColumnName("GPU");

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(255)
                    .HasColumnName("IP_Address");

                entity.Property(e => e.LastSuccesfullScan)
                    .HasColumnType("date")
                    .HasColumnName("Last_Succesfull_Scan");

                entity.Property(e => e.Manufacturer)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Model).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Os)
                    .HasMaxLength(255)
                    .HasColumnName("OS");

                entity.Property(e => e.Ram).HasColumnName("RAM");

                entity.Property(e => e.SerialNumber)
                    .HasMaxLength(255)
                    .HasColumnName("Serial_Number");

                entity.Property(e => e.Version).HasMaxLength(255);
            });

            modelBuilder.Entity<DeviceSoftware>(entity =>
            {
                entity.HasKey(e => new { e.DeviceId, e.SoftwareId });

                entity.ToTable("Device_Software");

                entity.Property(e => e.DeviceId).HasColumnName("DeviceID");

                entity.Property(e => e.SoftwareId).HasColumnName("SoftwareID");

                entity.Property(e => e.InstallDate).HasColumnType("date");

                entity.Property(e => e.LicenseId).HasColumnName("LicenseID");

                entity.HasOne(d => d.Device)
                    .WithMany(p => p.DeviceSoftwares)
                    .HasForeignKey(d => d.DeviceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Device_Software_Device");

                entity.HasOne(d => d.License)
                    .WithMany(p => p.DeviceSoftwares)
                    .HasForeignKey(d => d.LicenseId)
                    .HasConstraintName("FK_Device_Software_License");

                entity.HasOne(d => d.Software)
                    .WithMany(p => p.DeviceSoftwares)
                    .HasForeignKey(d => d.SoftwareId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Device_Software_Software");
            });

            modelBuilder.Entity<License>(entity =>
            {
                entity.ToTable("License");

                entity.Property(e => e.LicenseId).HasColumnName("LicenseID");

                entity.Property(e => e.LicenseKey)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("Start_Date");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Report");

                entity.Property(e => e.ReportId).HasColumnName("ReportID");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("End_Date");

                entity.Property(e => e.SoftwareId).HasColumnName("SoftwareID");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("Start_Date");

                entity.Property(e => e.Type).HasMaxLength(255);

                entity.HasOne(d => d.Software)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.SoftwareId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Report_Software");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Software>(entity =>
            {
                entity.ToTable("Software");

                entity.Property(e => e.SoftwareId).HasColumnName("SoftwareID");

                entity.Property(e => e.AccId).HasColumnName("AccID");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Docs).HasMaxLength(255);

                entity.Property(e => e.Download).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Os)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("OS");

                entity.Property(e => e.Publisher).HasMaxLength(255);

                entity.Property(e => e.Release).HasMaxLength(255);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Version).HasMaxLength(255);

                entity.HasOne(d => d.Acc)
                    .WithMany(p => p.Softwares)
                    .HasForeignKey(d => d.AccId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Software_Account");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
