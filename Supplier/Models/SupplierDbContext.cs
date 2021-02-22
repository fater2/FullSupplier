using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supplier.Models
{
    public class SupplierDbContext:DbContext
    {
        public SupplierDbContext(DbContextOptions<SupplierDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryProduct>().HasKey(sp => new { sp.CategoryId, sp.ProductId });
            //modelBuilder.Entity<Product>()
            //    .HasMany(b => b.images)
            //    .WithOne()
            //    .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Category>()
                .HasMany(b => b.images)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

        }
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSpecification> ProductSpecifications { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<CategorySpecification> CategorySpecifications { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }
    }
}
