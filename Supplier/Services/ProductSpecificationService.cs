using Microsoft.AspNetCore.Mvc;
using Supplier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supplier.Services.IServices
{
    public class ProductSpecificationService<TEntity> : IProductSpecificationServices<TEntity> where TEntity : ProductSpecification
    {
        private readonly SupplierDbContext _context;

        public ProductSpecificationService(SupplierDbContext context)
        {
            _context = context;
        }
        public IEnumerable<ProductSpecification> GetProductSpecifications()
        {
            return _context.ProductSpecifications;
        }
        public TEntity Find(int id)
        {

            var entity = _context.ProductSpecifications.Find(id);
            //entities.Find(id);
            return (TEntity)entity;

        }
    }
}
