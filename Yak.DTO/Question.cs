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
        public DateTime CreateDate { get; set; }
        public DateTime LastModificationDate { get; set; }

        public string AuthorName
        {
            get
            {
                if (Author != null)
                {
                    return Author.Username;
                }

                return string.Empty;
            }
        }

        [ElasticProperty(OptOut = true)]
        public User Author { get; set; }

        [ElasticProperty(OptOut = true)]
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
            Author = question.Author != null ? new User(question.Author) : null;
            Tags = question.Tags.Select(t => new Tag(t)).ToList();
        }
    }
}
