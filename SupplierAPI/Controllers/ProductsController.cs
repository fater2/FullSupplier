using System;
using System.Collections.Generic;
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
        private readonly IGenericRepository<Product> _repository;
        //private readonly IMapper _mapper;
        public ProductsController(IGenericRepository<Product> repository)
        {
            //_mapper = mapper;
            this._repository = repository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            var items = _repository.List();
            return Ok(items);

        }
    }
}
