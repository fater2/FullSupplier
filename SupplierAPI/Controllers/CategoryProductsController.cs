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
    public class CategoryProductsController : ControllerBase
    {
        private readonly ICategoryProductServices<CategoryProduct> _services;
        //private readonly IMapper _mapper;
        public CategoryProductsController(ICategoryProductServices<CategoryProduct> services)
        {
            //_mapper = mapper;
            this._services = services;
        }
        [HttpGet]
        public ActionResult<IEnumerable<CategoryProduct>> GetAllProducts()
        {
            return Ok(_services.GetCategoryProducts());
        }
        [HttpGet("{category_id}/{product_id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CategoryProduct> GetById(int category_id, int product_id)
        {
            try
            {
                var entity = _services.Find(category_id,product_id);

                if (entity == null)
                {
                    return BadRequest("entity Not Found");
                }
                else
                    return Ok(entity);
            }
            catch (Exception)
            {
                return BadRequest("entity Not Found");
            }
        }
    }
}
