using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Supplier.Interfaces;
using Supplier.Models;
using Supplier.Services;
using Supplier.Services.IServices;

namespace SupplierAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
         {
            services.AddDbContext<SupplierDbContext>(options =>
                                                       options.UseSqlServer(Configuration.GetConnectionString("SupplierConnection")
                                                       ).UseLazyLoadingProxies());
            services.AddControllers();
            services.AddControllers().AddNewtonsoftJson(options =>
             options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            //services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            services.AddScoped<ICategoryServices<Category>, CategoryService<Category>>();
            services.AddScoped<IProductServices<Product>, ProductService<Product>>();
            services.AddScoped<ICategoryProductServices<CategoryProduct>, CategoryProductService<CategoryProduct>>();
            services.AddScoped<IProductSpecificationServices<ProductSpecification>, ProductSpecificationService<ProductSpecification>>();
            services.AddScoped<ISpecificationServices<Specification>, SpecificationService<Specification>>();
            services.AddScoped<SupplierDbContext>();
            //swagger
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                // c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
