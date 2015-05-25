using System;
using System.Reflection;
using Yak.DTO;
using Yak.Services.Interfaces;

namespace Yak.TestDataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new LightInject.ServiceContainer();
            container.RegisterAssembly(Assembly.GetExecutingAssembly());
            container.BeginScope();
            var questionService = container.GetInstance<ISearchEngineExtendedService<Question>>();
            var userService = container.GetInstance<IService<User>>();

            for (int i = 0; i < 10000; i++)
            {
                questionService.Add(new Question
                {
                    Title = Faker.Lorem.Sentence(5),
                    Content =  Faker.Lorem.Paragraph(10),
                    Author = userService.GetById(1).Username,
                    CreateDate = DateTime.Now,
                    LastModificationDate = DateTime.Now
                });
                Console.WriteLine("Adding question number " + i);
            }
            container.EndCurrentScope();
        }
    }
}
