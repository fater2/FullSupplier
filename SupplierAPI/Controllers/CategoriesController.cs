using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supplier.Interfaces;
using Supplier.Models;
using Supplier.Services.IServices;

namespace SupplierAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryServices<Category> _services;
        //private readonly IMapper _mapper;
        public CategoriesController(ICategoryServices<Category> services)
        {
            //_mapper = mapper;
            this._services = services;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAllCategories()
        {
            return Ok(_services.GetCategories());
        }
    }
}
