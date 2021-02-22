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
    public class SpecificationsController : Controller
    {
        private readonly SupplierDbContext _context;

        public SpecificationsController(SupplierDbContext context)
        {
            _context = context;
        }

        // GET: Specifications
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var supplierDbContext = _context.Specifications.Include(s => s.CategorySpecification).Include(s => s.Product);
            return View(await supplierDbContext.ToListAsync());
        }

        // GET: Specifications/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specification = await _context.Specifications
                .Include(s => s.CategorySpecification)
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specification == null)
            {
                return NotFound();
            }

            return View(specification);
        }

        // GET: Specifications/Create
        public IActionResult Create()
        {
            ViewData["CategorySpecificationId"] = new SelectList(_context.CategorySpecifications, "Id", "Name");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: Specifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,value,ProductId,CategorySpecificationId")] Specification specification)
        {
            if (ModelState.IsValid)
            {
                _context.Add(specification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategorySpecificationId"] = new SelectList(_context.CategorySpecifications, "Id", "Name", specification.CategorySpecificationId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", specification.ProductId);
            return View(specification);
        }

        // GET: Specifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specification = await _context.Specifications.FindAsync(id);
            if (specification == null)
            {
                return NotFound();
            }
            ViewData["CategorySpecificationId"] = new SelectList(_context.CategorySpecifications, "Id", "Name", specification.CategorySpecificationId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", specification.ProductId);
            return View(specification);
        }

        // POST: Specifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,value,ProductId,CategorySpecificationId")] Specification specification)
        {
            if (id != specification.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(specification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecificationExists(specification.Id))
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
            ViewData["CategorySpecificationId"] = new SelectList(_context.CategorySpecifications, "Id", "Id", specification.CategorySpecificationId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", specification.ProductId);
            return View(specification);
        }

        // GET: Specifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specification = await _context.Specifications
                .Include(s => s.CategorySpecification)
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specification == null)
            {
                return NotFound();
            }

            return View(specification);
        }

        // POST: Specifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var specification = await _context.Specifications.FindAsync(id);
            _context.Specifications.Remove(specification);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecificationExists(int id)
        {
            return _context.Specifications.Any(e => e.Id == id);
        }
    }
}
