using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace TheZtack.Services.Interfaces
{
    public interface IService<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        IEnumerable<T> Filter(Func<T, bool> predicate);
        int Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
