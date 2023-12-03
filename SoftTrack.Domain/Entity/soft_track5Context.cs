using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace SoftTrack.Domain
{
    public partial class soft_track5Context : DbContext
    {
        public soft_track5Context()
        {
        }
        public soft_track5Context(DbContextOptions<soft_track5Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<AssetApplication> AssetApplications { get; set; }
        public virtual DbSet<AssetSoftware> AssetSoftwares { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Library> Libraries { get; set; }
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

            modelBuilder.Entity<Application>(entity =>
            {
                entity.HasKey(e => e.AppId);

                entity.ToTable("Application");

                entity.Property(e => e.AppId).HasColumnName("AppID");

                entity.Property(e => e.AccId).HasColumnName("AccID");

                entity.Property(e => e.Db)
                    .HasMaxLength(255)
                    .HasColumnName("DB");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Docs).HasMaxLength(255);

                entity.Property(e => e.Download).HasMaxLength(255);

                entity.Property(e => e.Language).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Os)
                    .HasMaxLength(255)
                    .HasColumnName("OS");

                entity.Property(e => e.Osversion)
                    .HasMaxLength(255)
                    .HasColumnName("OSVersion");

                entity.Property(e => e.Publisher).HasMaxLength(255);

                entity.Property(e => e.Release).HasMaxLength(255);

                entity.Property(e => e.Type).HasMaxLength(255);

                entity.Property(e => e.Version)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Acc)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.AccId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Application_Account");
            });

            modelBuilder.Entity<Asset>(entity =>
            {
                entity.ToTable("Asset");

                entity.Property(e => e.AssetId).HasColumnName("AssetID");

                entity.Property(e => e.Bandwidth).HasMaxLength(255);

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

                entity.Property(e => e.Manufacturer).HasMaxLength(255);

                entity.Property(e => e.Memory).HasMaxLength(255);

                entity.Property(e => e.Model).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Os)
                    .HasMaxLength(255)
                    .HasColumnName("OS");

                entity.Property(e => e.Ram)
                    .HasMaxLength(255)
                    .HasColumnName("RAM");

                entity.Property(e => e.SerialNumber)
                    .HasMaxLength(255)
                    .HasColumnName("Serial_Number");

                entity.Property(e => e.Version).HasMaxLength(255);
            });

            modelBuilder.Entity<AssetApplication>(entity =>
            {
                entity.HasKey(e => new { e.AssetId, e.AppId });

                entity.ToTable("Asset_Application");

                entity.Property(e => e.AssetId).HasColumnName("AssetID");

                entity.Property(e => e.AppId).HasColumnName("AppID");

                entity.Property(e => e.InstallDate).HasColumnType("date");

                entity.HasOne(d => d.App)
                    .WithMany(p => p.AssetApplications)
                    .HasForeignKey(d => d.AppId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Asset_Application_Application");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.AssetApplications)
                    .HasForeignKey(d => d.AssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Asset_Application_Asset");
            });

            modelBuilder.Entity<AssetSoftware>(entity =>
            {
                entity.HasKey(e => new { e.AssetId, e.SoftwareId });

                entity.ToTable("Asset_Software");

                entity.Property(e => e.AssetId).HasColumnName("AssetID");

                entity.Property(e => e.SoftwareId).HasColumnName("SoftwareID");

                entity.Property(e => e.InstallDate).HasColumnType("date");

                entity.Property(e => e.LicenseId).HasColumnName("LicenseID");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.AssetSoftwares)
                    .HasForeignKey(d => d.AssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Asset_Software_Asset");

                entity.HasOne(d => d.License)
                    .WithMany(p => p.AssetSoftwares)
                    .HasForeignKey(d => d.LicenseId)
                    .HasConstraintName("FK_Asset_Software_License");

                entity.HasOne(d => d.Software)
                    .WithMany(p => p.AssetSoftwares)
                    .HasForeignKey(d => d.SoftwareId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Asset_Software_Software");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("Image");

                entity.Property(e => e.ImageId).HasColumnName("ImageID");

                entity.Property(e => e.Image1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Image");

                entity.Property(e => e.ReportId).HasColumnName("ReportID");

                //entity.HasOne(d => d.Report)
                //    .WithMany(p => p.Images)
                //    .HasForeignKey(d => d.ReportId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Image_Report");
            });

            modelBuilder.Entity<Library>(entity =>
            {
                entity.ToTable("Library");

                entity.Property(e => e.LibraryId).HasColumnName("LibraryID");

                entity.Property(e => e.AppId).HasColumnName("AppID");

                entity.Property(e => e.LibraryKey)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Publisher)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("Start_Date");

                entity.HasOne(d => d.App)
                    .WithMany(p => p.Libraries)
                    .HasForeignKey(d => d.AppId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Library_Application");
            });

            modelBuilder.Entity<License>(entity =>
            {
                entity.ToTable("License");

                entity.Property(e => e.LicenseId).HasColumnName("LicenseID");

                entity.Property(e => e.LicenseKey).HasMaxLength(255);

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("Start_Date");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Report");

                entity.Property(e => e.ReportId).HasColumnName("ReportID");
                entity.Property(e => e.AccId).HasColumnName("AccID");
                entity.Property(e => e.AppId).HasColumnName("AppID");

                entity.Property(e => e.Description).HasMaxLength(255);
                

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("End_Date");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("Start_Date");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(255);


                entity.HasOne(d => d.App)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.AppId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Report_Application");
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

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Os)
                    .HasMaxLength(255)
                    .HasColumnName("OS");

                entity.Property(e => e.Publisher)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Release).HasMaxLength(255);

                entity.Property(e => e.Type).HasMaxLength(255);

                entity.Property(e => e.Version)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
