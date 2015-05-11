using System;
using System.Collections.Generic;

namespace YAK.Base.Data.Interfaces
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
