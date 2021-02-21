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
    public class CategoriesController : ControllerBase
    {
        private readonly IGenericRepository<Category> _repository;
        //private readonly IMapper _mapper;
        public CategoriesController(IGenericRepository<Category> repository)
        {
            //_mapper = mapper;
            this._repository = repository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAllProducts()
        {
            var items = _repository.List();
            return Ok(items);

        }
    }
}
