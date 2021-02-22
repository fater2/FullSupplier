using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supplier.Models;
using Supplier.Services.IServices;

namespace Supplier.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
       
        private readonly SupplierDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductsController(SupplierDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;

        }

        // GET: Products
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var products = _context.Products;
            return View(await _context.Products.ToListAsync());
        }
        //get products by their category
        [AllowAnonymous]
        public IActionResult CategoryProducts(int id)
        {
            var category = _context.Categories.Include(m => m.Products)
                .ThenInclude(m => m.Product)
                .FirstOrDefault(m => m.Id == id);
            if(category!=null)
            ViewBag.Text = "Products Of " + category.Name;
            List <Product> products =new List<Product>();
            foreach(var a in category.Products)
            {
                products.Add(a.Product);
            }
            return View(products);
           // return View(products.ToList());
        }
        // GET: Products/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(m=>m.ProductSpecifications)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Name,ProductionDate,ExpireDate,Amount,Price,ImageFile")] Product entity)
        {
            if (ModelState.IsValid)
            {
                //save image t o root/image
                string wwwrootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(entity.ImageFile.FileName);
                string extention = Path.GetExtension(entity.ImageFile.FileName);
              
                entity.ImageName= fileName= fileName + DateTime.Now.ToString("yymmssfff") + extention;
          
                string path = Path.Combine(wwwrootPath + "/image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await entity.ImageFile.CopyToAsync(fileStream);
                }


                _context.Add(entity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(entity);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Name,ProductionDate,ExpireDate,Amount,Price,ImageFile")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Products
            .FirstOrDefaultAsync(m => m.Id == id);
            //delete image from folder image
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "image", item.ImageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
            //delete from database 

                _context.Products.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
