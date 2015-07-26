using Yak.DTO;

namespace Yak.Services.Interfaces
{
    public interface IUserValidationService : IService<User>
    {
        bool Validate(string username, string password);
    }
}
