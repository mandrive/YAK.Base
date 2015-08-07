using System;
using System.Collections.Generic;
using System.Linq;
using Yak.Database;
using Yak.DTO;
using Yak.Services.Interfaces;

namespace Yak.Services
{
    public class CommentService : IService<Comment>
    {
        private DatabaseContext _databaseContext;

        public CommentService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void Add(Comment entity)
        {
            var dbEntity = new Database.Entities.Comment
            {
                Id = entity.Id,
                Content = entity.Content,
                CreateDate = DateTime.UtcNow,
                LastModificationDate = DateTime.UtcNow,
                RankPoint = entity.RankPoint,
                Author = _databaseContext.Users.Find(entity.Author.Id),
                Votes = entity.Votes != null ? entity.Votes.Select(v => _databaseContext.Votes.Find(v.Id)).ToList() : new List<Database.Entities.Vote>(),
                Question = entity.QuestionId.HasValue ? _databaseContext.Questions.Find(entity.QuestionId.Value) : null,
                Answer = entity.AnswerId.HasValue ? _databaseContext.Answers.Find(entity.AnswerId.Value) : null,
            };

            _databaseContext.Comments.Add(dbEntity);
            _databaseContext.SaveChanges();

            entity.Id = dbEntity.Id;
        }

        public void Delete(Comment entity)
        {
            var comment = _databaseContext.Comments.Find(entity.Id);
            _databaseContext.Comments.Remove(comment);
        }

        public IEnumerable<Comment> Filter(Func<Comment, bool> filter)
        {
            return GetAll().Where(filter);
        }

        public IEnumerable<Comment> GetAll()
        {
            return _databaseContext.Comments.Select(t => new Comment(t));
        }

        public Comment GetById(int id)
        {
            var dbComment = _databaseContext.Comments.Find(id);
            if (dbComment == null) return null;

            return new Comment(dbComment);
        }

        public void Update(Comment entity)
        {
            var dbComment = _databaseContext.Comments.Find(entity.Id);

            dbComment.Content = entity.Content;
            dbComment.CreateDate = entity.CreateDate;
            dbComment.LastModificationDate = entity.LastModificationDate;
            dbComment.RankPoint = entity.RankPoint;
            dbComment.Votes = entity.Votes != null ? entity.Votes.Select(v => _databaseContext.Votes.Find(v.Id)).ToList() : new List<Database.Entities.Vote>();
            dbComment.Author = entity.Author != null ? _databaseContext.Users.Find(entity.Author.Id) : dbComment.Author;
            dbComment.Answer = entity.AnswerId.HasValue ? _databaseContext.Answers.Find(entity.AnswerId.Value) : null;
            dbComment.Question = entity.QuestionId.HasValue ? _databaseContext.Questions.Find(entity.QuestionId.Value) : null;

            _databaseContext.SaveChanges();
        }
    }
}
