using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Yak.Services.Interfaces
{
    public interface ISearchEngineExtendedService<T> : IService<T> where T : class
    {
        IEnumerable<T> GetFromIndex(params string[] searchValues);
    }
}
