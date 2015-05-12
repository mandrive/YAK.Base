using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Yak.Database.Entities;

namespace Yak.Database.Migrations
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
            var firstUser = new User { Id = 1, Username = "admin", Password = "Password", Email = "admin@admintest.com" };

            context.Users
                .AddOrUpdate(
                firstUser,
                new User { Id = 2, Username = "£ukasz Wasak", Password = "Password", Email = "lwasak@mail.com" },
                new User { Id = 3, Username = "Maciej Kozera", Password = "Password", Email = "mkozerak@mail.com" },
                new User { Id = 4, Username = "Kajetan Targonski", Password = "Password", Email = "ktargonski@mail.com" }
                );

            var question = new Question
                {
                    Id = 1,
                    Title = "asd",
                    Content = "asd",
                    LastModificationDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Author = firstUser,
                    RankPoint = 1,
                    Votes = new List<Vote>()
                };

            context.Questions.AddOrUpdate(question);

            var vote = new Vote
                {
                    Id = 1,
                    PointValue = true,
                    User = firstUser,
                    Questions = new List<Question>()
                };

            question.Votes.Add(vote);

            context.SaveChanges();
        }
    }
}
