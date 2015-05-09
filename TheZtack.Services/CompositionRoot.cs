using LightInject;
using TheZtack.Data.Interfaces;
using TheZtack.Data.Services;
using TheZtack.Database.Entities;

namespace TheZtack.Data
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IService<Question>, QuestionService>(new PerRequestLifeTime());
        }
    }
}
