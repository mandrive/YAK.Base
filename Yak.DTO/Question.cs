using Nest;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public string Author { get; set; }

        public List<Tag> Tags { get; set; }

        public Question()
        {
        }

        public Question(Database.Entities.Question question)
        {
            Id = question.Id;
            Title = question.Title;
            Content = question.Content;
            CreateDate = question.CreateDate;
            LastModificationDate = question.LastModificationDate;
            Author = question.Author.Username;
            Tags = question.Tags.Select(t => new Tag(t)).ToList();
            }
        }
    }
}
