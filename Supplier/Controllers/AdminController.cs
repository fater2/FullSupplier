using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Supplier.Data;
using Supplier.Models;

namespace Supplier.Controllers
{
    public class AdminController : Controller
    {
        private readonly SupplierIdentityDbContext _adminContext;

        private readonly SupplierDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminController(SupplierIdentityDbContext adminContext/*, UserManager<IdentityUser> userManager*/, SupplierDbContext context/*, RoleManager<IdentityRole> roleManager*/)
        {
            _context = context;
            _adminContext = adminContext;
            //_userManager = userManager;
            //_roleManager = roleManager;

        }
        public IActionResult Index()
        {


            var CurrUserId = GetUserId();
            if (CurrUserId != null)
            {
                var user = _adminContext.Admins.FirstOrDefault(m => m.Id == GetUserId());
                ViewData["UserFirstName"] = user.Firstname;
                //ViewData["LastName"] = user.LastName;
            }


            return View();
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}

