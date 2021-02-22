using Supplier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supplier.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        IList<TEntity> List();
        TEntity Find(int id);
        void Add(TEntity entity);

        void Update(TEntity entity);
        void Delete(int id);
        List<TEntity> Search(string term);
    }
}
