using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SoftTrack.Domain
{
    public partial class Do_AnContext : DbContext
    {
        public Do_AnContext()
        {
        }

        public Do_AnContext(DbContextOptions<Do_AnContext> options)
            : base(options)
        {
        }

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
            modelBuilder.Entity<Software>(entity =>
            {
                entity.ToTable("Software");

                entity.Property(e => e.SoftwareId)
                    .ValueGeneratedNever()
                    .HasColumnName("SoftwareID");

                entity.Property(e => e.InstallDate).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Publisher).HasMaxLength(100);

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.Property(e => e.Version).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
