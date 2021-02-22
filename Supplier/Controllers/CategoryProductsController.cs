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
    public class CategoryProductsController : Controller
    {
        private readonly SupplierDbContext _context;

        public CategoryProductsController(SupplierDbContext context)
        {
            _context = context;
        }

        // GET: CategoryProducts
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var supplierDbContext = _context.CategoryProducts.Include(c => c.Category).Include(c => c.Product);
            return View(await supplierDbContext.ToListAsync());
        }

        // GET: CategoryProducts/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryProduct = await _context.CategoryProducts
                .Include(c => c.Category)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (categoryProduct == null)
            {
                return NotFound();
            }

            return View(categoryProduct);
        }

        // GET: CategoryProducts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }
        public IActionResult CreateWithCategoryId(int id)
        {
            var x = _context.Categories.Find(id);
            
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View(x);
        }
       

        // POST: CategoryProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,ProductId")] CategoryProduct categoryProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", categoryProduct.CategoryId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", categoryProduct.ProductId);
            return View(categoryProduct);
        }
        /*
         * 
         * 
         * 
         * 
         * 
         */
          // POST: CategoryProducts/CreateWithCategoryId
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWithCategoryId([Bind("Id,CategoryId,Products")] Category categoryProduct)
        {

            if (ModelState.IsValid)
            {
                var id = categoryProduct.Id;
                var updatedCategory=_context.Categories.Find(id);
                foreach(var newProd in categoryProduct.Products)
                {
                    Product newProduct = _context.Products.Find(newProd.ProductId);
                    CategoryProduct cp = new CategoryProduct();
                    cp.ProductId = categoryProduct.Id;
                    cp.ProductId = newProd.ProductId;
                    _context.Add(cp);
                }
               
                //categoryProduct.ProductId = 0;
                //_context.Add(categoryProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", categoryProduct.CategoryId);
            //ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", categoryProduct.ProductId);
            return View(categoryProduct);
        }

         /* 
         * 
         * 
         * 
         * 
         * 
         * 
         * */
        // GET: CategoryProducts/Edit/5
        public async Task<IActionResult> Edit(int CategoryId , int ProductId)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var categoryProduct = await _context.CategoryProducts.FirstOrDefaultAsync(m => m.ProductId==ProductId && m.CategoryId==CategoryId);
            if (categoryProduct == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", categoryProduct.CategoryId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", categoryProduct.ProductId);
            return View(categoryProduct);
        }

        // POST: CategoryProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int CategoryId, int ProductId,[Bind("CategoryId,ProductId")] CategoryProduct categoryProduct)
        {
            //if (CategoryId != categoryProduct.CategoryId)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryProductExists(categoryProduct.CategoryId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", categoryProduct.CategoryId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", categoryProduct.ProductId);
            return View(categoryProduct);
        }

        // GET: CategoryProducts/Delete/5
        public async Task<IActionResult> Delete(int CategoryId, int ProductId)
        {
           
         

            var categoryProduct = await _context.CategoryProducts
                .Include(c => c.Category)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m =>( m.CategoryId == CategoryId&& ProductId==m.ProductId));
            if (categoryProduct == null)
            {
                return NotFound();
            }

            return View(categoryProduct);
        }

        // POST: CategoryProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int CategoryId, int ProductId)
        {
            var categoryProduct = await _context.CategoryProducts.FirstOrDefaultAsync(m => (m.CategoryId == CategoryId && ProductId == m.ProductId));
            _context.CategoryProducts.Remove(categoryProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryProductExists(int id)
        {
            return _context.CategoryProducts.Any(e => e.CategoryId == id);
        }
    }
}
