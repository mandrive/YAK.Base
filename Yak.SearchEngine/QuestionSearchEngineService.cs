using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using Yak.DTO;
using System.Linq.Expressions;
using Yak.SearchEngine.Interfaces;

namespace Yak.SearchEngine
{
    public class QuestionSearchEngineService : ISearchEngineService<Question>
    {
        private ElasticClient _elasticClient;

        public QuestionSearchEngineService()
        {
            var setting = new ConnectionSettings(new Uri("http://localhost:9200"), defaultIndex: "yakbase");
            _elasticClient = new ElasticClient(setting);
        }

        public void AddToIndex(Question indexObject)
        {
            _elasticClient.Index(indexObject);
        }

        public IEnumerable<Question> GetAll()
        {
            var allHits = _elasticClient.Search<Question>(q => q.AllIndices()).Hits.Select(h => h.Source);

            return allHits;
        }

        public Question GetById(int id)
        {
            var result = _elasticClient.Search<Question>(s => s.Query(q => q.Term(p => p.Id, id)));

            if (result.Hits.Count() > 0)
            {
                return result.Hits.FirstOrDefault().Source;
            }

            return null;
        }

        public IEnumerable<Question> GetFiltered(params string[] searchValues)
        {
            if (searchValues == null)
            {
                throw new Exception("No search values passed!");
            }

            if (searchValues.Count() > 1)
            {
                throw new Exception("Only one search value is needed!");
            }

            var searchValue = searchValues[0];

            var hits = _elasticClient.Search<Question>(s => s.Query(q => q.Match(m => m.OnField(i => i.Title).Query(searchValue).Analyzer("standard")) || q.Match(m => m.OnField(i => i.Content).Query(searchValue).Analyzer("standard")))).Hits;
            var result = hits.Select(h => h.Source);

            return result;
        }
    }
}
