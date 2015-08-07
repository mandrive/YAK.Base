using System;
using System.Collections.Generic;
using System.Linq;

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
        public int QuestionId { get; set; }

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
            Comments = answer.Comments != null ? answer.Comments.Select(c => new Comment(c)).ToList() : new List<Comment>();
            Votes = answer.Votes != null ? answer.Votes.Select(v => new Vote(v)).ToList() : new List<Vote>();
            QuestionId = answer.Question.Id;
        }
    }
}
