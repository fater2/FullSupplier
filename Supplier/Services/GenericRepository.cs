using Microsoft.EntityFrameworkCore;
using Supplier.Interfaces;
using Supplier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supplier.Services
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected SupplierDbContext context;
        protected DbSet<TEntity> entities;


        public GenericRepository(SupplierDbContext context)
        {
            this.context = context;
            entities = context.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                entities.Add(entity);
                context.SaveChanges();

            }
            catch (Exception e)
            {
                throw e;
            }

        }

       
        public TEntity Find(int id)
        {
            var entity = entities.Find(id);
            return entity;


        }
        public IList<TEntity> List()
        {

            return context.Set<TEntity>().ToList<TEntity>();

        }
        public List<TEntity> Search(string term)
        {
            throw new NotImplementedException();
        }
        public void Update(TEntity entity)
        {

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            try
            {
                context.Set<TEntity>().Update(entity);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }




        }
        public void Delete(int id)
        {

            var entity = entities.Find(id);

            if (entity == null)
            {
                throw new ArgumentNullException("Entity you try to delete isn't exist");
            }
            entities.Remove(entity);
            context.SaveChanges();

        }

    }
}
