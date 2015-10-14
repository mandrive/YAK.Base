using System;
using System.Collections.Generic;
using System.Linq;
using Yak.Database;
using Yak.DTO;
using Yak.Services.Interfaces;
using Tag = Yak.Database.Entities.Tag;

namespace Yak.Services
{
    public class QuestionService : IService<Question>
    {
        private ISearchEngineExtendedService<Question> _searchEngineService;
        private DatabaseContext _databaseContext;

        public QuestionService(DatabaseContext databaseContext, ISearchEngineExtendedService<Question> searchEngineService)
        {
            _searchEngineService = searchEngineService;
            _databaseContext = databaseContext;
        }

        public void Add(Question entity)
        {
            var dbEntity = new Database.Entities.Question
            {
                Title = entity.Title,
                Content = entity.Content,
                CreateDate = entity.CreateDate,
                LastModificationDate = entity.LastModificationDate,
                Author = _databaseContext.Users.Single(u => u.Id == entity.Author.Id)
            };

            _databaseContext.Questions.Add(dbEntity);
            _databaseContext.SaveChanges();

            entity.Id = dbEntity.Id;

            AddQuestionTags(entity.Tags, dbEntity);

            _searchEngineService.Add(new Question(dbEntity));
        }

        public void Delete(Question entity)
        {
            var dbEntity = _databaseContext.Questions.Find(entity.Id);

            _databaseContext.Questions.Remove(dbEntity);
            _databaseContext.SaveChanges();
        }

        public IEnumerable<Question> Filter(Func<Question, bool> filter)
        {
            return _searchEngineService.Filter(filter);
        }

        public IEnumerable<Question> GetAll()
        {
            var questions = _databaseContext.Questions
                .Include("Tags")
                .Include("Answers")
                .Include("Comments")
                .Include("Votes")
                .ToList();

            return questions.Select(q => new Question(q));
        }

        public Question GetById(int id)
        {
            return new Question(_databaseContext.Questions.Find(id));
        }

        public void Update(Question entity)
        {
            var dbEntity = _databaseContext.Questions.Find(entity.Id);

            dbEntity.Content = entity.Content;
            dbEntity.Title = entity.Title;
            dbEntity.RankPoint = entity.RankPoint;
            dbEntity.Votes = entity.Votes != null ? entity.Votes.Select(v => _databaseContext.Votes.Find(v.Id)).ToList() : dbEntity.Votes;
            dbEntity.LastModificationDate = DateTime.UtcNow;

            if (entity.Tags != null)
            {
                dbEntity.Tags.Clear();
                AddQuestionTags(entity.Tags, dbEntity);
            }

            _databaseContext.SaveChanges();

            _searchEngineService.Update(new Question(dbEntity));
        }

        private void AddQuestionTags(IList<DTO.Tag> tags, Database.Entities.Question entity)
        {
            if (tags != null && tags.Count > 0)
            {
                var tagNames = tags.Select(x => x.Name);
                var dbTags = _databaseContext.Tags.Where(t => tagNames.Contains(t.Name));

                if (entity.Tags == null)
                {
                    entity.Tags = new List<Tag>();
                }

                foreach (var tag in tags)
                {
                    var currentTag = tag;
                    if (dbTags.Any(t => t.Name == currentTag.Name))
                    {
                        var dbTag = dbTags.Single(t => t.Name == currentTag.Name);
                        entity.Tags.Add(dbTag);
                    }
                    else
                    {
                        var newTag = new Tag { Name = currentTag.Name };
                        _databaseContext.Tags.Add(newTag);
                        entity.Tags.Add(newTag);
                        _databaseContext.SaveChanges();
                    }
                }
            }
        }
    }
}
