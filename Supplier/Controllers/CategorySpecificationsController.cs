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
    public class CategorySpecificationsController : Controller
    {
        private readonly SupplierDbContext _context;

        public CategorySpecificationsController(SupplierDbContext context)
        {
            _context = context;
        }

        // GET: CategorySpecifications
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var supplierDbContext = _context.CategorySpecifications.Include(c => c.Category);
            return View(await supplierDbContext.ToListAsync());
        }

        [AllowAnonymous]
        public async Task<IActionResult> IndexForId(int id)
        {
            var supplierDbContext = _context.CategorySpecifications.Where(p => p.CategoryId == id);
            supplierDbContext = supplierDbContext.Include(m => m.Category);
            //return RedirectToAction("Index", "ProductSpecifications");
            return View("Index", await supplierDbContext.ToListAsync());
        }

        // GET: CategorySpecifications/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorySpecification = await _context.CategorySpecifications
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categorySpecification == null)
            {
                return NotFound();
            }

            return View(categorySpecification);
        }

        // GET: CategorySpecifications/Create
        /*public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }*/

        public IActionResult Create(int id)
        {
            var x = new CategorySpecification();
            x.CategoryId = id;
            ViewData["CategoryId"] = _context.Categories.FirstOrDefault(m => m.Id == id).Id;
            return View(x);
        }

        // POST: CategorySpecifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,isValued,CategoryId")] CategorySpecification categorySpecification)
        {
            if (ModelState.IsValid)
            {
                categorySpecification.Id = 0;
                _context.Add(categorySpecification);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Categories");
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", categorySpecification.CategoryId);
            return View(categorySpecification);
        }

        // GET: CategorySpecifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorySpecification = await _context.CategorySpecifications.FindAsync(id);
            if (categorySpecification == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", categorySpecification.CategoryId);
            return View(categorySpecification);
        }
              
        // POST: CategorySpecifications/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,isValued,CategoryId")] CategorySpecification categorySpecification)
        {
          
            if (id != categorySpecification.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categorySpecification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategorySpecificationExists(categorySpecification.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", categorySpecification.CategoryId);
            return View(categorySpecification);
        }

        // GET: CategorySpecifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorySpecification = await _context.CategorySpecifications
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categorySpecification == null)
            {
                return NotFound();
            }

            return View(categorySpecification);
        }

        // POST: CategorySpecifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categorySpecification = await _context.CategorySpecifications.FindAsync(id);
            _context.CategorySpecifications.Remove(categorySpecification);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategorySpecificationExists(int id)
        {
            return _context.CategorySpecifications.Any(e => e.Id == id);
        }
    }
}
