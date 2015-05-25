using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using Yak.DTO;
using Yak.SearchEngine.Interfaces;

namespace Yak.SearchEngine
{
    public class QuestionSearchEngineService : ISearchEngineService<Question>
    {
        private readonly ElasticClient _elasticClient;

        public QuestionSearchEngineService()
        {
            var setting = new ConnectionSettings(new Uri("http://localhost:9200"), "yakbase");
            _elasticClient = new ElasticClient(setting);
        }

        public void AddToIndex(Question indexObject)
        {
            _elasticClient.Index(indexObject);
        }

        public IEnumerable<Question> GetAll()
        {
            var allHits = _elasticClient.Search<Question>(q => q.SearchType(SearchType.Scan).Scroll("1m"));
            var realHits = _elasticClient.Scroll<Question>("1m", allHits.ScrollId).Hits.Select(h => h.Source);

            return realHits;
        }

        public Question GetById(int id)
        {
            var result = _elasticClient.Search<Question>(s => s.Query(q => q.Term(p => p.Id, id)));

            if (!result.Hits.Any()) return null;
            var firstOrDefault = result.Hits.FirstOrDefault();

            return firstOrDefault != null ? firstOrDefault.Source : null;
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
            var hits = _elasticClient.Search<Question>(s => s.Analyzer("standard").Query(q => q.MultiMatch(mm => mm.OnFields(i => i.Content, i => i.Title).Query(searchValue).Type(TextQueryType.CrossFields).Operator(Operator.And)))).Hits;
            var result = hits.Select(h => h.Source);

            return result;
        }
    }
}
