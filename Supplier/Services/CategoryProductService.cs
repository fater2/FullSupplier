using Microsoft.AspNetCore.Mvc;
using Supplier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supplier.Services.IServices
{
    public class CategoryProductService<TEntity> : ICategoryProductServices<TEntity> where TEntity : CategoryProduct
    {
        private readonly SupplierDbContext _context;

        public CategoryProductService(SupplierDbContext context)
        {
            _context = context;
        }
        public IEnumerable<CategoryProduct> GetCategoryProducts()
        {
            return _context.CategoryProducts;
        }
        public TEntity Find(int category_id,int product_id)
        {

            var entity = _context.CategoryProducts.Find(category_id,product_id);
            //entities.Find(id);
            return (TEntity)entity;

        }
    }
}
