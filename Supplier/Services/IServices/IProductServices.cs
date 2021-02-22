using Supplier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supplier.Services.IServices
{
    public interface IProductServices <TEntity> where TEntity: BaseEntity
    {
        public IEnumerable<Product> GetProducts();
        public TEntity Find(int id);
    }
}
