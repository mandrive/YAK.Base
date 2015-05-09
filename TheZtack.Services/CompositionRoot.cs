using LightInject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheZtack.Database.Entities;
using TheZtack.Services.Interfaces;

namespace TheZtack.Services
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IService<Question>, QuestionService>(new PerRequestLifeTime());
        }
    }
}
