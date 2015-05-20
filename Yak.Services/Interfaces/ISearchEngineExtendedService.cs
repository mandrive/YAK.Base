using System.Collections.Generic;

namespace Yak.Services.Interfaces
{
    public interface ISearchEngineExtendedService<T> : IService<T> where T : class
    {
        IEnumerable<T> GetFromIndex(params string[] searchValues);
    }
}
