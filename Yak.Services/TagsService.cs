using System;
using System.Collections.Generic;
using System.Linq;
using Yak.Database;
using Yak.DTO;
using Yak.Services.Interfaces;

namespace Yak.Services
{
    public class TagsService : IService<Tag>
    {
        private DatabaseContext _databaseContext;

        public TagsService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void Add(Tag entity)
        {
            var dbEntity = new Database.Entities.Tag
            {
                Name = entity.Name
            };

            _databaseContext.Tags.Add(dbEntity);
            _databaseContext.SaveChanges();

            entity.Id = dbEntity.Id;
        }

        public void Delete(Tag entity)
        {
            var tag = _databaseContext.Tags.Find(entity.Id);
            _databaseContext.Tags.Remove(tag);
        }

        public IEnumerable<Tag> Filter(Func<Tag, bool> filter)
        {
            return GetAll().Where(filter);
        }

        public IEnumerable<Tag> GetAll()
        {
            return _databaseContext.Tags.Select(t => new Tag(t));
        }

        public Tag GetById(int id)
        {
            var dbTag = _databaseContext.Tags.Find(id);
            if (dbTag == null) return null;

            return new Tag(dbTag);
        }

        public void Update(Tag entity)
        {
            throw new NotImplementedException();
        }
    }
}
