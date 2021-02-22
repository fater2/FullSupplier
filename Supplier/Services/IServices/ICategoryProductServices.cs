using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Supplier.Models;
namespace Supplier.Services.IServices
{
   public interface ICategoryProductServices<TEntity> where TEntity: BaseEntity
    {
        /*Task<Category> Add(Category entity);
        IList<Category> List();
        Category Find(int id);
        

        void Update(Category entity);
        void Delete(int id);
        List<Category> Search(string term);*/
 //       IList<TEntity> list();
       public IEnumerable<CategoryProduct> GetCategoryProducts();
       public TEntity Find(int category_id,int product_id);
    }
}
