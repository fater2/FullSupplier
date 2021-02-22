using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Supplier.Models;

namespace Supplier.Controllers
{
    
    public class HomeController : Controller
    {
        public SupplierDbContext SupplierDbContext;


        public HomeController(SupplierDbContext supplierDbContext)
        {
            this.SupplierDbContext = supplierDbContext;
        }


        public IActionResult Index()
        {
         
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Services()
        {
            return View();
        }
        public IActionResult Categories()
        {
            var categories = SupplierDbContext.Categories.Include(m => m.images);
            return View(categories);
        }
        public IActionResult Contact()
        {
            
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
