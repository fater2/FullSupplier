﻿using System;
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
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices<Product> _services;
        //private readonly IMapper _mapper;
        public ProductsController(IProductServices<Product> services)
        {
            //_mapper = mapper;
            this._services = services;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            return Ok(_services.GetProducts());
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Product> GetById(int id)
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
