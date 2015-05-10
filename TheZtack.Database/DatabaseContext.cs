using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TheZtack.Database.Entities;
using TheZtack.Database.Migrations;

namespace TheZtack.Database
{
    public class DatabaseContext : DbContext
    {
        static DatabaseContext()
        {
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Configuration>());
        }

        public DatabaseContext() : base("ztackConnectionString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<User>()
                .HasMany(u => u.Questions)
                .WithRequired(q => q.Author)
                .Map(x => x.MapKey("UserId"));

            modelBuilder.Entity<User>()
                .HasMany(u => u.Answers)
                .WithRequired(a => a.Author)
                .Map(x => x.MapKey("UserId"));

            modelBuilder.Entity<User>()
                .HasMany(u => u.Comments)
                .WithRequired(c => c.Author)
                .Map(x => x.MapKey("UserId"));

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithRequired()
                .Map(x => x.MapKey("QuestionId"))
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Comments)
                .WithOptional()
                .Map(x => x.MapKey("QuestionId"))
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Tags)
                .WithMany(t => t.Questions)
                .Map(
                    x => x.MapLeftKey("QuestionId")
                          .MapRightKey("TagId"));

            modelBuilder.Entity<Question>()
                .HasMany(q => q.RankingPoints)
                .WithMany(t => t.Questions)
                .Map(
                    x => x.MapLeftKey("QuestionId")
                          .MapRightKey("RankingPointId"));

            modelBuilder.Entity<Answer>()
                .HasMany(q => q.RankingPoints)
                .WithMany(t => t.Answers)
                .Map(
                    x => x.MapLeftKey("AnswerId")
                          .MapRightKey("RankingPointId"));

            modelBuilder.Entity<Comment>()
                .HasMany(q => q.RankingPoints)
                .WithMany(t => t.Comments)
                .Map(
                    x => x.MapLeftKey("CommentId")
                          .MapRightKey("RankingPointId"));

            modelBuilder.Entity<User>()
                .HasMany(u => u.RankingPoints)
                .WithRequired(c => c.User)
                .Map(x => x.MapKey("UserId"));

            modelBuilder.Entity<Answer>()
                .HasMany(a => a.Comments)
                .WithOptional()
                .Map(x => x.MapKey("AnswerId"))
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RankingPoint> RankingPoints { get; set; }
    }
}
