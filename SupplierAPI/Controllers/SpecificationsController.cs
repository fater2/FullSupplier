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
    public class SpecificationsController : ControllerBase
    {
        private readonly ISpecificationServices<Specification> _services;
        //private readonly IMapper _mapper;
        public SpecificationsController(ISpecificationServices<Specification> services)
        {
            //_mapper = mapper;
            this._services = services;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Specification>> GetAllSpecifications()
        {
            return Ok(_services.GetSpecifications());
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Specification> GetById(int id)
        {
            try
            {
                var entity = _services.Find(id);

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
