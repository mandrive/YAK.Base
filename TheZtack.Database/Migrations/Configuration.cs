using System.Data.Entity.Migrations;
using System.Linq;
using TheZtack.Database.Entities;

namespace TheZtack.Database.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        private readonly bool _areTherePendingMigrations;

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;

            var migrator = new DbMigrator(this);
            _areTherePendingMigrations = migrator.GetPendingMigrations().Any();
        }

        protected override void Seed(DatabaseContext context)
        {
            if (!_areTherePendingMigrations)
            {
                return;
            }

            context.Users
                .AddOrUpdate(
                new User { Username = "£ukasz Wasak", Password = "Password", Email = "lwasak@mail.com" },
                new User { Username = "Maciej Kozera", Password = "Password", Email = "mkozerak@mail.com" },
                new User { Username = "Kajetan Targonski", Password = "Password", Email = "ktargonski@mail.com" }
                );

            context.SaveChanges();
        }
    }
}
