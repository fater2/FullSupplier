﻿using Microsoft.AspNetCore.Mvc;
using Supplier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supplier.Services.IServices
{
    public class ProductService<TEntity> : IProductServices<TEntity> where TEntity : Product
    {
        private readonly SupplierDbContext _context;

        public ProductService(SupplierDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> GetProducts()
        {
            return _context.Products;
        }
        public TEntity Find(int id)
        {

            var entity = _context.Products.Find(id);
            //entities.Find(id);
            return (TEntity)entity;

        }
    }
}
