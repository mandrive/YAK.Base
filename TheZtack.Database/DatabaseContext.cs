using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Database.Initialize(false);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        } 

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users{ get; set; } 
    }
}
