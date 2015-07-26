using System.Linq;
using System.Security.Principal;
using Yak.DTO;
using Yak.Services.Interfaces;
using Yak.Web.Interfaces;

namespace Yak.Web.Models
{
    public class CustomPrincipal : ICustomPrincipal
    {
        private IService<User> _userService;

        public CustomPrincipal(IService<User> userService, IIdentity identity)
        {
            _userService = userService;
            Identity = identity;
        }

        public bool IsInRole(string role)
        {
            return false;
        }

        public IIdentity Identity { get; private set; }

        private User _databaseUser;
        public User DatabaseUser
        {
            get
            {
                return _databaseUser ??
                       (_databaseUser =
                           _userService.Filter(u => u.Username == Identity.Name.Split('\\')[1]).SingleOrDefault());
            }
        }
    }
}