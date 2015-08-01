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
                CreateDate = entity.CreateDate,
                LastModificationDate = entity.CreateDate,
                RankPoint = entity.RankPoint,
                Author = _databaseContext.Users.Find(entity.Author.Id),
                Votes = entity.Votes.Select(v => _databaseContext.Votes.Find(v.Id)).ToList(),
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
            throw new NotImplementedException();
        }
    }
}
