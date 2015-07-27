using System;
using System.Collections.Generic;
using System.Linq;
using Yak.Database;
using Yak.DTO;
using Yak.SearchEngine.Interfaces;
using Yak.Services.Interfaces;
using Tag = Yak.Database.Entities.Tag;

namespace Yak.Services
{
    public class QuestionService : ISearchEngineExtendedService<Question>
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ISearchEngineService<Question> _questionSearchEngineService;

        public QuestionService(DatabaseContext databaseContext, ISearchEngineService<Question> questionSearchEngineService)
        {
            _databaseContext = databaseContext;
            _questionSearchEngineService = questionSearchEngineService;
        }

        public Question GetById(int id)
        {
            return new Question(_databaseContext.Questions.Find(id));
        }

        public IEnumerable<Question> GetAll()
        {
            return _questionSearchEngineService.GetAll();
        }

        public IEnumerable<Question> Filter(Func<Question, bool> filter)
        {
            return _questionSearchEngineService.GetAll().Where(filter);
        }

        public void Add(Question dto)
        {
            var dbEntity = new Database.Entities.Question
            {
                Title = dto.Title,
                Content = dto.Content,
                CreateDate = dto.CreateDate,
                LastModificationDate = dto.LastModificationDate,
                Author = _databaseContext.Users.Single(u => u.Id == dto.Author.Id)
            };

            _databaseContext.Questions.Add(dbEntity);
            _databaseContext.SaveChanges();

            dto.Id = dbEntity.Id;

            AddQuestionTags(dto.Tags, dbEntity);

            _questionSearchEngineService.AddToIndex(new Question(dbEntity));
        }

        public void Delete(Question dto)
        {
            var dbEntity = _databaseContext.Questions.Find(dto.Id);

            _databaseContext.Questions.Remove(dbEntity);
            _databaseContext.SaveChanges();
        }

        public void Update(Question dto)
        {
            var dbEntity = _databaseContext.Questions.Find(dto.Id);

            dbEntity.Content = dto.Content;
            dbEntity.Title = dto.Title;
            dbEntity.LastModificationDate = DateTime.Now;

            dbEntity.Tags.Clear();

            AddQuestionTags(dto.Tags, dbEntity);

            _databaseContext.SaveChanges();

            _questionSearchEngineService.AddToIndex(new Question(dbEntity));
        }

        public IEnumerable<Question> GetFromIndex(string query)
        {
            return _questionSearchEngineService.GetFiltered(query);
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
                        _databaseContext.SaveChanges();

                        entity.Tags.Add(newTag);
                    }
                }
            }
        }
    }
}
