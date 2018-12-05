using System;
using System.Collections.Generic;
using System.Text;

namespace MexicanFood.Core.DomainService
{
    public interface IRepository<T>
    {
        IEnumerable<T> ReadAll();

        T ReadById(int id);

        T CreateEntity(T entity);

        T UpdateEntity(T entity);

        T DeleteEntity(int id);
    }
}
