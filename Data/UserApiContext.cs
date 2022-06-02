using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UserApi.Data
{
    public class UserApiContext : IdentityDbContext<ApiUser, IdentityRole, string>
    {
        public UserApiContext() { }
        public UserApiContext(DbContextOptions<UserApiContext> options) : base(options)
        {
            //eDatabase.Migrate();
        }
        //public DbSet<Level> Level { get; set; }
        //public DbSet<Commentaire> Commentaire { get; set; }
        //public DbSet<Module> Module { get; set; }
        //public DbSet<Notion> Notion { get; set; }
        //public DbSet<Question> Question { get; set; }
        //public DbSet<Response> Response { get; set; }
        //public DbSet<Source> Source { get; set; }
        //public DbSet<Tag> Tag { get; set; }
        //public DbSet<VoteQuestion> VoteQuestion { get; set; }
        //public DbSet<VoteResponse> VoteResponse { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<NewdleUser>(u => {
            //    u.HasKey(u => u.Id);
            //    u.HasOne(u => u.Level).WithMany(l => l.Users).HasForeignKey(u => u.IdLevel).OnDelete(DeleteBehavior.Restrict);
            //    u.HasMany(u => u.VotesQuestions).WithOne(v => v.User).HasForeignKey(u => u.IdUser).OnDelete(DeleteBehavior.Cascade);
            //    u.HasMany(u => u.VotesResponses).WithOne(v => v.User).HasForeignKey(u => u.IdUser).OnDelete(DeleteBehavior.Cascade);
            //});

            //builder.Entity<Notion>(n => {
            //    n.HasKey(n => n.IdNotion);
            //    n.HasMany(n => n.Sources).WithOne(s => s.Notion).HasForeignKey(n => n.IdNotion).OnDelete(DeleteBehavior.NoAction);
            //    n.HasMany(n => n.Questions).WithOne(q => q.Notion).HasForeignKey(n => n.IdNotion);
            //    n.HasMany(n => n.Tags).WithMany(t => t.Notions).UsingEntity<Dictionary<string, object>>("TagsNotions",
            //        j => j.HasOne<Tag>().WithMany().OnDelete(DeleteBehavior.Cascade),
            //        j => j.HasOne<Notion>().WithMany().OnDelete(DeleteBehavior.Cascade));
            //});

            //builder.Entity<Module>(m =>
            //{
            //    m.HasKey(m => m.IdModule);
            //    m.HasMany(m => m.Notions).WithOne(n => n.Module).HasForeignKey(n => n.IdModule);
            //    m.HasMany(m => m.Questions).WithOne(q => q.Module).HasForeignKey(q => q.IdModule);
            //    m.HasMany(m => m.Tags).WithMany(t => t.Modules).UsingEntity<Dictionary<string, object>>("TagsModules",
            //        j => j.HasOne<Tag>().WithMany().OnDelete(DeleteBehavior.Cascade),
            //        j => j.HasOne<Module>().WithMany().OnDelete(DeleteBehavior.Cascade));

            //    m.HasIndex(m => m.Name).IsUnique();
            //});

            //builder.Entity<Question>(q =>
            //{
            //    q.HasKey(q => q.IdQuestion);
            //    q.HasMany(q => q.Responses).WithOne(r => r.Question).HasForeignKey(r => r.IdQuestion);
            //    q.HasMany(q => q.Commentaires).WithOne(c => c.Question).HasForeignKey(c => c.IdQuestion);
            //    q.HasOne(q => q.Author).WithMany(u => u.Questions).HasForeignKey(q => q.IdAuthor).OnDelete(DeleteBehavior.NoAction);
            //    q.HasMany(q => q.Tags).WithMany(t => t.Questions).UsingEntity<Dictionary<string, object>>("TagsQuestions",
            //        j => j.HasOne<Tag>().WithMany().OnDelete(DeleteBehavior.Cascade),
            //        j => j.HasOne<Question>().WithMany().OnDelete(DeleteBehavior.Cascade));
            //    q.HasMany(q => q.Votes).WithOne(v => v.Question).HasForeignKey(q => q.QuestionId).OnDelete(DeleteBehavior.Cascade);

            //    q.HasIndex(q => q.Title).IsUnique();
            //});

            //builder.Entity<VoteQuestion>(vq =>
            //{
            //    vq.HasKey(vq => vq.IdVote);
            //    vq.HasOne(vq => vq.Question).WithMany(q => q.Votes).HasForeignKey(vq => vq.IdVote);
            //});

            //builder.Entity<VoteResponse>(vr =>
            //{
            //    vr.HasKey(vr => vr.IdVote);
            //    vr.HasOne(vr => vr.Response).WithMany(r => r.Votes).HasForeignKey(vr => vr.IdVote);
            //});

            //builder.Entity<Commentaire>(c =>
            //{
            //    c.HasKey(c => c.IdCommentaire);
            //    c.HasOne(c => c.Author).WithMany(u => u.Commentaires).HasForeignKey(c => c.IdAuthor).OnDelete(DeleteBehavior.NoAction);
            //});

            //builder.Entity<Level>(l =>
            //{
            //    l.HasKey(l => l.IdLevel);

            //    l.HasIndex(l => l.Name).IsUnique();
            //});

            //builder.Entity<Response>(r =>
            //{
            //    r.HasKey(r => r.IdResponse);
            //    r.HasOne(r => r.Author).WithMany(u => u.Responses).HasForeignKey(r => r.IdAuthor).OnDelete(DeleteBehavior.NoAction);
            //    r.HasMany(r => r.Votes).WithOne(v => v.Response).HasForeignKey(r => r.ReponseId).OnDelete(DeleteBehavior.Cascade);
            //});

            //builder.Entity<Source>(s =>
            //{
            //    s.HasKey(s => s.IdSource);
            //});

            //builder.Entity<Tag>(t =>
            //{
            //    t.HasKey(t => t.IdTag);


            //    t.HasIndex(t => t.Name).IsUnique();
            //});
        }
    }
}
