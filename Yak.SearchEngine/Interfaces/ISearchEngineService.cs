using System.Collections.Generic;

namespace Yak.SearchEngine.Interfaces
{
    public interface ISearchEngineService<T> where T : class
    {
        void AddToIndex(T indexObject);
        IEnumerable<T> GetAll();
        T GetById(int id);
        IEnumerable<T> GetFiltered(params string[] searchValues);
    }
}
