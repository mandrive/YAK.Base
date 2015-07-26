using LightInject;
using Yak.DTO;
using Yak.Services.Interfaces;
using Yak.Services.Utils;

namespace Yak.Services
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<ISearchEngineExtendedService<Question>, QuestionService>(new PerRequestLifeTime());
            serviceRegistry.Register<IService<User>, UserService>();
            serviceRegistry.Register<IUserValidationService, UserService>();
            serviceRegistry.Register<IndexRebuilder>(new PerRequestLifeTime());
        }
    }
}
