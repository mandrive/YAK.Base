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
        public DateTime CreateDate { get; set; }
        public DateTime LastModificationDate { get; set; }
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
            Author = new User(question.Author);
        }
    }
}
