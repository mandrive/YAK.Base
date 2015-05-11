using LightInject;
using YAK.Base.Data.Interfaces;
using YAK.Base.Data.Services;
using YAK.Base.Database.Entities;

namespace YAK.Base.Data
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IService<Question>, QuestionService>(new PerRequestLifeTime());
        }
    }
}
