using Microsoft.AspNetCore.Mvc;
using Supplier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supplier.Services.IServices
{
    public class SpecificationService<TEntity> : ISpecificationServices<TEntity> where TEntity : Specification
    {
        private readonly SupplierDbContext _context;

        public SpecificationService(SupplierDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Specification> GetSpecifications()
        {
            return _context.Specifications;
        }
        public TEntity Find(int id)
        {

            var entity = _context.Specifications.Find(id);
            //entities.Find(id);
            return (TEntity)entity;

        }
    }
}
