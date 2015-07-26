using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Yak.Database.Entities;

namespace Yak.Database.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DatabaseContext context)
        {
            var firstUser = new User { Id = 1, Username = "admin", Password = "e6c83b282aeb2e022844595721cc00bbda47cb24537c1779f9bb84f04039e1676e6ba8573e588da1052510e3aa0a32a9e55879ae22b0c2d62136fc0a3e85f8bb", Email = "admin@admintest.com" };

            context.Users
                .AddOrUpdate(
                firstUser,
                new User { Id = 2, Username = "£ukasz Wasak", Password = "e6c83b282aeb2e022844595721cc00bbda47cb24537c1779f9bb84f04039e1676e6ba8573e588da1052510e3aa0a32a9e55879ae22b0c2d62136fc0a3e85f8bb", Email = "lwasak@mail.com" },
                new User { Id = 3, Username = "Maciej Kozera", Password = "e6c83b282aeb2e022844595721cc00bbda47cb24537c1779f9bb84f04039e1676e6ba8573e588da1052510e3aa0a32a9e55879ae22b0c2d62136fc0a3e85f8bb", Email = "mkozerak@mail.com" },
                new User { Id = 4, Username = "Kajetan Targonski", Password = "e6c83b282aeb2e022844595721cc00bbda47cb24537c1779f9bb84f04039e1676e6ba8573e588da1052510e3aa0a32a9e55879ae22b0c2d62136fc0a3e85f8bb", Email = "ktargonski@mail.com" }
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
