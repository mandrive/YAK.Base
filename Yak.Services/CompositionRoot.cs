using LightInject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yak.DTO;
using Yak.Services.Interfaces;

namespace Yak.Services
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IService<Question>, QuestionService>(new PerRequestLifeTime());
            serviceRegistry.Register<IService<User>, UserService>(new PerRequestLifeTime());
        }
    }
}
