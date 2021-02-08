using System.Collections.Generic;

namespace ProductCatalog.Domain.Interfaces.Services
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        bool Add(TEntity obj);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        bool Update(TEntity obj);
        bool Remove(TEntity obj);
    }
}
