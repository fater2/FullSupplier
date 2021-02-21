using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Supplier.Interfaces;
using Supplier.Models;

namespace SupplierAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly SupplierDbContext _repository;
        //private readonly IMapper _mapper;
        public ProductsController(SupplierDbContext repository)
        {
            //_mapper = mapper;
            this._repository = repository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            /*return Ok(_repository.Products.Include(p => p.Categories)
                .Include()
                .ToList());*/
            return Ok(_repository.Products);

        }
    }

    /*
     * 
     * void Add(object entity);
        IList<object> List();
        object Find(int id);


        void Update(object entity);
        void Delete(int id);
        List<object> Search(string term);
        void Delete(Category id);
     * 
     * 
     * 
     * */
}
