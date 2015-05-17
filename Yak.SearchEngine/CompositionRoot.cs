using LightInject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yak.DTO;
using Yak.SearchEngine.Interfaces;

namespace Yak.SearchEngine
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<ISearchEngineService<Question>, QuestionSearchEngineService>(new PerContainerLifetime());
        }
    }
}
