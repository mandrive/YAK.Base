using LightInject;

namespace Yak.Database
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<DatabaseContext>(new PerRequestLifeTime());
        }
    }
}
