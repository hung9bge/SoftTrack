using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace SoftTrack.Domain
{
    public partial class soft_trackContext : DbContext
    {
        public soft_trackContext()
        {
        }

        public soft_trackContext(DbContextOptions<soft_trackContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<Issue> Issues { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleAccount> RoleAccounts { get; set; }
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
                entity.HasKey(e => e.AccId)
                    .HasName("PK__Account__91CBC398EE27C994");

                entity.ToTable("Account");

                entity.Property(e => e.AccId).HasColumnName("AccID");

                entity.Property(e => e.Account1)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Account");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Device>(entity =>
            {
                entity.ToTable("Device");

                entity.Property(e => e.DeviceId).HasColumnName("DeviceID");

                entity.Property(e => e.AccId).HasColumnName("AccID");

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("IP_Address");

                entity.Property(e => e.LastSuccessfullScan)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Last_Successfull_Scan");

                entity.Property(e => e.MacAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("MAC_Address");

                entity.Property(e => e.Manufacturer)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Model)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SerialNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Serial_Number");

                entity.HasOne(d => d.Acc)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.AccId)
                    .HasConstraintName("FK__Device__AccID__403A8C7D");
            });

            modelBuilder.Entity<Issue>(entity =>
            {
                entity.ToTable("Issue");

                entity.Property(e => e.IssueId).HasColumnName("IssueID");

                entity.Property(e => e.Category)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SoftwareId).HasColumnName("SoftwareID");

                entity.HasOne(d => d.Software)
                    .WithMany(p => p.Issues)
                    .HasForeignKey(d => d.SoftwareId)
                    .HasConstraintName("FK__Issue__SoftwareI__412EB0B6");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RoleAccount>(entity =>
            {
                entity.ToTable("Role_Account");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccId).HasColumnName("AccID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Acc)
                    .WithMany(p => p.RoleAccounts)
                    .HasForeignKey(d => d.AccId)
                    .HasConstraintName("FK__Role_Acco__AccID__4222D4EF");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleAccounts)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Role_Acco__RoleI__4316F928");
            });

            modelBuilder.Entity<Software>(entity =>
            {
                entity.ToTable("Software");

                entity.Property(e => e.SoftwareId).HasColumnName("SoftwareID");

                entity.Property(e => e.DeviceId).HasColumnName("DeviceID");

                entity.Property(e => e.InstallDate)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Publisher)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Version)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Device)
                    .WithMany(p => p.Softwares)
                    .HasForeignKey(d => d.DeviceId)
                    .HasConstraintName("FK__Software__Device__440B1D61");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
