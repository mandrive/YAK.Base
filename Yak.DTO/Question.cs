using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities = Yak.Database.Entities;

namespace Yak.DTO
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        [ElasticProperty(OptOut = true)]
        public DateTime CreateDate { get; set; }
        [ElasticProperty(OptOut = true)]
        public DateTime LastModificationDate { get; set; }
        [ElasticProperty(OptOut = true)]
        public User Author { get; set; }

        public Question()
        {
        }

        public Question(Entities.Question question)
        {
            Id = question.Id;
            Title = question.Title;
            Content = question.Content;
            CreateDate = question.CreateDate;
            LastModificationDate = question.LastModificationDate;
            if (question.Author != null)
            {
                Author = new User(question.Author);
            }
        }
    }
}
