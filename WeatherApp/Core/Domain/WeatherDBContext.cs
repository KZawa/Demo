using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WeatherApp.Core.Domain
{
    public partial class WeatherDBContext : DbContext
    {
        public WeatherDBContext()
        {
        }

        public WeatherDBContext(DbContextOptions<WeatherDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<WeatherMeasures> WeatherMeasures { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //              optionsBuilder.UseSqlServer("Data Source=(localdb)\\TestDB;Initial Catalog=WeatherDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<Cities>(entity =>
            {
                entity.HasKey(e => e.CityId)
                    .HasName("PK__Cities__DE9CEC388EE926C3");

                entity.HasIndex(e => e.CityName)
                    .HasName("pinky")
                    .IsUnique();

                entity.Property(e => e.CityId).HasColumnName("City_id");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasColumnName("City_name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(100);

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WeatherMeasures>(entity =>
            {
                entity.HasIndex(e => new { e.MeasureDate, e.CityId })
                    .HasName("ucCodes")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CityId).HasColumnName("city_id");

                entity.Property(e => e.Humidity).HasColumnName("humidity");

                entity.Property(e => e.MeasureDate)
                    .HasColumnName("measure_date")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Rain)
                    .HasColumnName("rain")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Snow)
                    .HasColumnName("snow")
                    .HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Temperature)
                    .HasColumnName("temperature")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Wind)
                    .HasColumnName("wind")
                    .HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.WeatherMeasures)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WeatherMeasures_Cities");
            });
        }
    }
}
