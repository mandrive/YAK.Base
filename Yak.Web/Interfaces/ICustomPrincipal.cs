using System.Security.Principal;
using Yak.DTO;

namespace Yak.Web.Interfaces
{
    public interface ICustomPrincipal : IPrincipal
    {
        User DatabaseUser { get; }
    }
}