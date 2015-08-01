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
            serviceRegistry.Register<ISearchEngineExtendedService<Question>, QuestionSearchEngineService>();
            serviceRegistry.Register<IService<Question>, QuestionService>();
            serviceRegistry.Register<IService<User>, UserService>();
            serviceRegistry.Register<IService<Tag>, TagsService>();
            serviceRegistry.Register<IService<Vote>, VoteService>();
            serviceRegistry.Register<IService<Answer>, AnswerService>();
            serviceRegistry.Register<IService<Comment>, CommentService>();
            serviceRegistry.Register<IUserValidationService, UserService>();
            serviceRegistry.Register<IndexRebuilder>(new PerRequestLifeTime());
        }
    }
}
