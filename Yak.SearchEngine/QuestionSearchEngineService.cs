using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using Yak.DTO;
using Yak.SearchEngine.Interfaces;
using System.Configuration;
using Yak.SearchEngine.Utils;

namespace Yak.SearchEngine
{
    public class QuestionSearchEngineService : ISearchEngineService<Question>
    {
        private readonly ElasticClient _elasticClient;

        public QuestionSearchEngineService()
        {
            try
            {
                var config = (ElasticConfiguration)ConfigurationManager.GetSection("elasticServer");
                var setting = new ConnectionSettings(new Uri(config.ElasticServer.ServerAddress), config.ElasticServer.IndexName);
                _elasticClient = new ElasticClient(setting);
            }
            catch(Exception ex)
            {
                throw ex;
            }
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

        public IEnumerable<Question> GetFiltered(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new Exception("No query passed!");
            }

            var results = _elasticClient.Search<Question>(
                s => s.Analyzer("standard")
                    .Query(q => q.MultiMatch(
                        mm => mm.OnFields(i => i.Content, i => i.Title)
                            .Query(query)
                            .Type(TextQueryType.CrossFields)
                            .Operator(Operator.And))))
                    .Hits
                    .Select(h => h.Source);

            return results;
        }
    }
}
