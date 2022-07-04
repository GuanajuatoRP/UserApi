using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UserApi.Data
{
    public partial class CarApiContext : DbContext
    {
        public CarApiContext()
        {
        }

        public CarApiContext(DbContextOptions<CarApiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; } = null!;
        public virtual DbSet<Maker> Makers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=127.0.0.1,1433;database=Cars;User Id=sa;Password=*5273%0&Q9%8q!3@#^1#");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(e => e.IdCar)
                    .HasName("PK_Car_1");

                entity.ToTable("Car");

                entity.HasIndex(e => e.MakerId, "IX_Car_Maker_Id");

                entity.Property(e => e.IdCar)
                    .HasColumnName("Id_Car")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Accelerate).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Aspiration).IsUnicode(false);

                entity.Property(e => e.Aviability).IsUnicode(false);

                entity.Property(e => e.Braking).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Class).IsUnicode(false);

                entity.Property(e => e.EngineConfiguration).IsUnicode(false);

                entity.Property(e => e.EngineDisplacement).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.EnginePosition).IsUnicode(false);

                entity.Property(e => e.Handling).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Launch).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.MakerId).HasColumnName("Maker_Id");

                entity.Property(e => e.Model).IsUnicode(false);

                entity.Property(e => e.Offroad).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Pi).HasColumnName("PI");

                entity.Property(e => e.PictureLink).IsUnicode(false);

                entity.Property(e => e.PowerBhp)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("Power_BHP");

                entity.Property(e => e.PowerKw)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("Power_KW");

                entity.Property(e => e.Rarity).IsUnicode(false);

                entity.Property(e => e.RequiredDlc)
                    .IsUnicode(false)
                    .HasColumnName("RequiredDLC");

                entity.Property(e => e.Speed).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.TorqueLbft)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("Torque_LBFT");

                entity.Property(e => e.TorqueNm)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("Torque_NM");

                entity.Property(e => e.Type).IsUnicode(false);

                entity.Property(e => e.WeightKg)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("Weight_KG");

                entity.Property(e => e.WeightLbs)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("Weight_LBS");

                entity.Property(e => e.WikiLink).IsUnicode(false);

                entity.HasOne(d => d.Maker)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.MakerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Car_Maker");
            });

            modelBuilder.Entity<Maker>(entity =>
            {
                entity.HasKey(e => e.IdMaker);

                entity.ToTable("Maker");

                entity.Property(e => e.IdMaker)
                    .HasColumnName("Id_Maker")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Origin)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Related)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.WikiLink)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
