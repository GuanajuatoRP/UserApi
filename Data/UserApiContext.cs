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
                v.HasKey(v => v.KeyCar);
            });

            builder.Entity<Markdown>(m =>
            {
                m.HasKey(m => m.TextId);
            });
        }
    }
}
