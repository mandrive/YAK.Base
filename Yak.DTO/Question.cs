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
        public int RankPoint { get; set; }
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
        
        public User Author { get; set; }

        public List<Tag> Tags { get; set; }

        public List<Comment> Comments { get; set; }

        public List<Answer> Answers { get; set; }

        public List<Vote> Votes { get; set; }

        public Question()
        {
        }

        public Question(Database.Entities.Question question)
        {
            Id = question.Id;
            Title = question.Title;
            Content = question.Content;
            CreateDate = question.CreateDate;
            RankPoint = question.RankPoint;
            LastModificationDate = question.LastModificationDate;
            Author = question.Author != null ? new User(question.Author) : null;
            Tags = question.Tags != null ? question.Tags.Select(t => new Tag(t)).ToList() : new List<Tag>();
            Answers = question.Answers != null ? question.Answers.Select(a => new Answer(a)).ToList() : new List<Answer>();
            Comments = question.Comments != null ? question.Comments.Select(c => new Comment(c)).ToList() : new List<Comment>();
            Votes = question.Votes != null ? question.Votes.Select(v => new Vote(v)).ToList() : new List<Vote>();
        }
    }
}
