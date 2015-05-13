using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Yak.Services.Interfaces
{
    public interface IService<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Filter(Func<T, bool> filter);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
