using System;
using System.Collections.Generic;

namespace Yak.DTO
{
    public class Answer
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int RankPoint { get; set; }
        public bool IsCorrect { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModificationDate { get; set; }
        public User Author { get; set; }
        public IList<Comment> Comments { get; set; }
        public IList<Vote> Votes { get; set; }

        public Answer()
        {
        }

        public Answer(Database.Entities.Answer answer)
        {
            Id = answer.Id;
            Content = answer.Content;
            RankPoint = answer.RankPoint;
            IsCorrect = answer.IsCorrect;
            CreateDate = answer.CreateDate;
            LastModificationDate = answer.LastModificationDate;
            Author = answer.Author != null ? new User(answer.Author) : null;
        }
    }
}
