using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace SoftTrack.Domain
{
    public partial class soft_track2Context : DbContext
    {
        public soft_track2Context()
        {
        }

        public soft_track2Context(DbContextOptions<soft_track2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<DeviceSoftware> DeviceSoftwares { get; set; }
        public virtual DbSet<Lisence> Lisences { get; set; }
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

                entity.Property(e => e.Ram).HasColumnName("RAM");

                entity.Property(e => e.SerialNumber)
                    .HasMaxLength(255)
                    .HasColumnName("Serial_Number");
            });

            modelBuilder.Entity<DeviceSoftware>(entity =>
            {
                entity.HasKey(e => new { e.DeviceId, e.SoftwareId });

                entity.ToTable("Device_Software");

                entity.Property(e => e.DeviceId).HasColumnName("DeviceID");

                entity.Property(e => e.SoftwareId).HasColumnName("SoftwareID");

                entity.Property(e => e.InstallDate).HasColumnType("date");

                entity.Property(e => e.LisenceId).HasColumnName("LisenceID");

                entity.HasOne(d => d.Device)
                    .WithMany(p => p.DeviceSoftwares)
                    .HasForeignKey(d => d.DeviceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Device_Software_Device");

                entity.HasOne(d => d.Lisence)
                    .WithMany(p => p.DeviceSoftwares)
                    .HasForeignKey(d => d.LisenceId)
                    .HasConstraintName("FK_Device_Software_Lisence");

                entity.HasOne(d => d.Software)
                    .WithMany(p => p.DeviceSoftwares)
                    .HasForeignKey(d => d.SoftwareId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Device_Software_Software");
            });

            modelBuilder.Entity<Lisence>(entity =>
            {
                entity.ToTable("Lisence");

                entity.Property(e => e.LisenceId).HasColumnName("LisenceID");

                entity.Property(e => e.LisenceKey)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.SoftwareId).HasColumnName("SoftwareID");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("Start_Date");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Report");

                entity.Property(e => e.ReportId)
                    .ValueGeneratedNever()
                    .HasColumnName("ReportID");

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

                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("RoleID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Software>(entity =>
            {
                entity.ToTable("Software");

                entity.Property(e => e.SoftwareId).HasColumnName("SoftwareID");

                entity.Property(e => e.AccId).HasColumnName("AccID");

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
