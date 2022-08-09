using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserApi.Controllers;

namespace UserApi.Data
{
    public class UserApiContext : IdentityDbContext<ApiUser, IdentityRole, string>
    {
        public UserApiContext() { }
        public UserApiContext(DbContextOptions<UserApiContext> options) : base(options)
        {
            //Database.Migrate();
        }
        public DbSet<Voitures> Voitures { get; set; }
        public DbSet<Stage> Stage { get; set; }
        public DbSet<Sessions> Sessions { get; set; }
        public DbSet<Markdown> Markdown { get; set; }
        public DbSet<OriginalCar> OriginalCars { get; set; } = null!;
        public DbSet<Maker> Makers { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApiUser>(u =>
            {
                u.HasKey(u => u.Id);
                u.HasOne(u => u.Stage).WithMany(s => s.Users).HasForeignKey(u => u.IdStage).OnDelete(DeleteBehavior.Restrict);
                u.HasMany(u => u.Voitures).WithOne(v => v.User).HasForeignKey(v => v.IdUser).OnDelete(DeleteBehavior.Cascade);
                u.HasMany(u => u.Sessions).WithMany(s => s.Users);
            });

            builder.Entity<Sessions>(s =>
            {
                s.HasKey(s => s.SessionId);
                s.Property( s => s.SessionNumber).ValueGeneratedOnAdd();
            });
            
            builder.Entity<Stage>(s =>
            {
                s.HasKey(s => s.StageId);
            });

            builder.Entity<Voitures>(v =>
            {
                v.ToTable("Voitures");
                v.HasKey(v => v.KeyCar);
                v.Property(e => e.Accelerate).HasColumnType("decimal(8, 2)");
                v.Property(e => e.Aspiration).IsUnicode(false);
                v.Property(e => e.Braking).HasColumnType("decimal(8, 2)");
                v.Property(e => e.Class).IsUnicode(false);
                v.Property(e => e.EngineDisplacement).HasColumnType("decimal(8, 2)");
                v.Property(e => e.EnginePosition).IsUnicode(false);
                v.Property(e => e.Handling).HasColumnType("decimal(8, 2)");
                v.Property(e => e.Launch).HasColumnType("decimal(8, 2)");
                v.Property(e => e.Offroad).HasColumnType("decimal(8, 2)");
                v.Property(e => e.Pi).HasColumnName("PI");
                v.Property(e => e.PowerBHP).HasColumnType("decimal(8, 2)");
                v.Property(e => e.PowerKW).HasColumnType("decimal(8, 2)");
                v.Property(e => e.Speed).HasColumnType("decimal(8, 2)");
                v.Property(e => e.TorqueLBFT).HasColumnType("decimal(8, 2)");
                v.Property(e => e.TorqueNM).HasColumnType("decimal(8, 2)");
                v.Property(e => e.WeightKG).HasColumnType("decimal(8, 2)");
                v.Property(e => e.WeightLBS).HasColumnType("decimal(8, 2)");
            });

            builder.Entity<Markdown>(m =>
            {
                m.HasKey(m => m.TextId);
            });

            builder.Entity<OriginalCar>(c =>
            {
                c.HasKey(e => e.IdCar)
                    .HasName("PK_Car_1");

                c.ToTable("OriginalCar");

                c.HasIndex(e => e.MakerId, "IX_Car_Maker_Id");

                c.Property(e => e.IdCar)
                    .HasColumnName("Id_Car")
                    .HasDefaultValueSql("(newid())");

                c.Property(e => e.Accelerate).HasColumnType("decimal(8, 2)");

                c.Property(e => e.Aspiration).IsUnicode(false);

                c.Property(e => e.Aviability).IsUnicode(false);

                c.Property(e => e.Braking).HasColumnType("decimal(8, 2)");

                c.Property(e => e.Class).IsUnicode(false);

                c.Property(e => e.EngineConfiguration).IsUnicode(false);

                c.Property(e => e.EngineDisplacement).HasColumnType("decimal(8, 2)");

                c.Property(e => e.EnginePosition).IsUnicode(false);

                c.Property(e => e.Handling).HasColumnType("decimal(8, 2)");

                c.Property(e => e.Launch).HasColumnType("decimal(8, 2)");

                c.Property(e => e.MakerId).HasColumnName("Maker_Id");

                c.Property(e => e.Model).IsUnicode(false);

                c.Property(e => e.Offroad).HasColumnType("decimal(8, 2)");

                c.Property(e => e.Pi).HasColumnName("PI");

                c.Property(e => e.PictureLink).IsUnicode(false);

                c.Property(e => e.PowerBhp)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("PowerBHP");

                c.Property(e => e.PowerKw)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("PowerKW");

                c.Property(e => e.Rarity).IsUnicode(false);

                c.Property(e => e.RequiredDlc)
                    .IsUnicode(false)
                    .HasColumnName("RequiredDLC");

                c.Property(e => e.Speed).HasColumnType("decimal(8, 2)");

                c.Property(e => e.TorqueLbft)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("TorqueLBFT");

                c.Property(e => e.TorqueNm)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("TorqueNM");

                c.Property(e => e.Type).IsUnicode(false);

                c.Property(e => e.WeightKg)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("WeightKG");

                c.Property(e => e.WeightLbs)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("WeightLBS");

                c.Property(e => e.WikiLink).IsUnicode(false);

                c.HasOne(d => d.Maker)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.MakerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Car_Maker");
            });

            builder.Entity<Maker>(m =>
            {
                m.HasKey(e => e.IdMaker);

                m.ToTable("Maker");

                m.Property(e => e.IdMaker)
                    .HasColumnName("Id_Maker")
                    .HasDefaultValueSql("(newid())");

                m.Property(e => e.Description).HasColumnType("text");

                m.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                m.Property(e => e.Origin)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                m.Property(e => e.Related)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                m.Property(e => e.WikiLink)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }
    }
}
