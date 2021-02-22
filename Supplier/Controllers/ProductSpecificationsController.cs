using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Supplier.Models;

namespace Supplier.Controllers
{
    [Authorize]
    public class ProductSpecificationsController : Controller
    {
        private readonly SupplierDbContext _context;

        public ProductSpecificationsController(SupplierDbContext context)
        {
            _context = context;
        }

        // GET: ProductSpecifications
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var supplierDbContext = _context.ProductSpecifications.Include(p => p.Product);
            return View(await supplierDbContext.ToListAsync());
        }
        [AllowAnonymous]
        public async Task<IActionResult> IndexForId(int id)
        {
            
            var supplierDbContext = _context.ProductSpecifications.Where(p => p.Product.Id == id);
            supplierDbContext = supplierDbContext.Include(m=> m.Product);
            //return RedirectToAction("Index", "ProductSpecifications");
            return View("Index",await supplierDbContext.ToListAsync());
        }

        // GET: ProductSpecifications/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSpecification = await _context.ProductSpecifications
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productSpecification == null)
            {
                return NotFound();
            }

            return View(productSpecification);
        }

        // GET: ProductSpecifications/Create
        public IActionResult Create(int id)
        {
            var x = new ProductSpecification();
            x.ProductId = id;
            ViewData["ProductId"] = _context.Products.FirstOrDefault(m=>m.Id==id).Id;
            return View(x);
        }

        // POST: ProductSpecifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name,value,ProductId")] ProductSpecification productSpecification)
        {
            if (ModelState.IsValid)
            {
                productSpecification.Id = 0;
                _context.Add(productSpecification);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Products");
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", productSpecification.ProductId);
            return RedirectToAction("Index","Products");
                //View(productSpecification);
        }

        // GET: ProductSpecifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSpecification = await _context.ProductSpecifications.FindAsync(id);
            if (productSpecification == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", productSpecification.Id);
            return View(productSpecification);
        }

        // POST: ProductSpecifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,value,ProductId")] ProductSpecification productSpecification)
        {
            if (id != productSpecification.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productSpecification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductSpecificationExists(productSpecification.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", productSpecification.ProductId);
            return View(productSpecification);
        }

        // GET: ProductSpecifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSpecification = await _context.ProductSpecifications
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productSpecification == null)
            {
                return NotFound();
            }

            return View(productSpecification);
        }

        // POST: ProductSpecifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productSpecification = await _context.ProductSpecifications.FindAsync(id);
            _context.ProductSpecifications.Remove(productSpecification);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductSpecificationExists(int id)
        {
            return _context.ProductSpecifications.Any(e => e.Id == id);
        }
    }
}
