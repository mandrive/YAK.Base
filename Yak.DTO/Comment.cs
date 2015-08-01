using System;
using System.Collections.Generic;

namespace Yak.DTO
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int RankPoint { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModificationDate { get; set; }
        public IList<Vote> Votes { get; set; }
        public User Author { get; set; }

        public Comment()
        {
        }

        public Comment(Database.Entities.Comment comment)
        {
            Id = comment.Id;
            Content = comment.Content;
            RankPoint = comment.RankPoint;
            CreateDate = comment.CreateDate;
            LastModificationDate = comment.LastModificationDate;
            Votes = new List<Vote>();
            Author = comment.Author != null ? new User(comment.Author) : null;

            foreach (var vote in comment.Votes)
            {
                Votes.Add(new Vote(vote));
            }
        }
    }
}
