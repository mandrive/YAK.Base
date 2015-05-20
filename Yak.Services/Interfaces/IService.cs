using System;
using System.Collections.Generic;

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
