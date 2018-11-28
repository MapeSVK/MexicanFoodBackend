using System;
using System.Collections.Generic;
using System.Text;

namespace MexicanFood.Core.DomainService
{
    public interface IRepository<T>
    {
        IEnumerable<T> ReadAllEntities();

        T EntityFoundById(int id);

        T CreateEntity(T entity);

        T UpdateEntity(int id, T entity);

        T DeleteEntity(int id);
    }
}
