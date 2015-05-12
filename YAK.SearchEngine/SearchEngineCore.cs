using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using Yak.Database.Entities;

namespace Yak.SearchEngine
{
    public class SearchEngineCore
    {
        private ElasticClient _elasticClient;

        public SearchEngineCore()
        {
            var setting = new ConnectionSettings(new Uri("http://localhost:9200"), defaultIndex: "Yakbase");
            _elasticClient = new ElasticClient(setting);
        }

        public void AddSomethingToIndex()
        {
            Random r = new Random();
            var newQuestion = new Question()
            {
                Id = 1,
                Title = r.Next().ToString(),
                Content = r.Next().ToString()
            };

            _elasticClient.Index(newQuestion);
        }

        public IEnumerable<Question> GetAllQuestions()
        {
            var allQuestions = _elasticClient.Search<Question>(q => q.AllIndices());
            var allHits = _elasticClient.Search<Question>(q => q.AllIndices()).Hits.ToList().Select(h => h.Source);

            return allHits;
        }
    }
}
