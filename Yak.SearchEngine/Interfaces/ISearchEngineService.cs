using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
