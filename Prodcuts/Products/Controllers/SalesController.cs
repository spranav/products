using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Products.Data;
using Products.Models;

namespace Products.Controllers
{
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;


        public JsonResult GetSubCategories(int categoryId)
        {
            var subCategories = _context.SubCategories.Where(s => s.CategoryId == categoryId).ToList();
            subCategories.Insert(0, new SubCategory { Id = 0, Name = "Select" });
            return Json(new SelectList(subCategories, "Id", "Name"));
        }

        public JsonResult GetProductModels(int subCategoryId)
        {
            var models = _context.ProductModels.Where(s => s.SubCategoryId == subCategoryId).ToList();
            models.Insert(0, new ProductModel { Id = 0, Name = "Select" });

            return Json(new SelectList(models, "Id", "Name"));
        }

        public JsonResult GetProducts(int productModelId)
        {
            var products = _context.Products.Where(s => s.ProductModelId == productModelId).ToList();
            products.Insert(0, new Product{ Id = 0, Name = "Select" });

            return Json(new SelectList(products, "Id", "Name"));
        }


        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sales
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Sales.Include(s => s.ModeOfPayment).Include(s=>s.Product.ProductModel.SubCategory.Category).Include(s => s.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .Include(s => s.ModeOfPayment)
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // GET: Sales/Create
        public IActionResult Create()
        {
            var categories = _context.Categories.ToList();
            categories.Insert(0, new Category { Id = 0, Name = "Select" });
            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");

            var subCategories = new List<SubCategory>();
            subCategories.Insert(0, new SubCategory {Id=0, Name="Select" });
            ViewData["SubCategoryId"] = new SelectList(subCategories, "Id", "Name");

            var productModels = new List<ProductModel>();
            productModels.Insert(0, new ProductModel { Id = 0, Name = "Select" });
            ViewData["ProductModelId"] = new SelectList(productModels, "Id", "Name");

            var products = new List<Product>();
            products.Insert(0, new Product{ Id = 0, Name = "Select" });
            ViewData["ProductId"] = new SelectList(products, "Id", "Name");

            ViewData["ModeOfPaymentId"] = new SelectList(_context.Set<ModeOfPayment>(), "Id", "Name");

            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,ModeOfPaymentId,Quantity,CucstomerName,MobileNumber,EmailID,Address,DateOfBilling")] Sale sale)
        {
            if(sale.ProductId <1)
            {
                ModelState.AddModelError("", "Please select a valid Product");
            }

            if (ModelState.IsValid)
            {
                _context.Add(sale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModeOfPaymentId"] = new SelectList(_context.Set<ModeOfPayment>(), "Id", "Id", sale.ModeOfPaymentId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", sale.ProductId);
            return View(sale);
        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
           

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["SubCategoryId"] = new SelectList(_context.SubCategories, "Id", "Name");
            ViewData["ProductModelId"] = new SelectList(_context.ProductModels, "Id", "Name");

            ViewData["ModeOfPaymentId"] = new SelectList(_context.Set<ModeOfPayment>(), "Id", "Name", sale.ModeOfPaymentId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", sale.ProductId);

            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,ModeOfPaymentId,Quantity,CucstomerName,MobileNumber,EmailID,Address,DateOfBilling")] Sale sale)
        {
            if (id != sale.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleExists(sale.Id))
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
            ViewData["ModeOfPaymentId"] = new SelectList(_context.Set<ModeOfPayment>(), "Id", "Name", sale.ModeOfPaymentId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", sale.ProductId);
            return View(sale);
        }

        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .Include(s => s.ModeOfPayment)
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleExists(int id)
        {
            return _context.Sales.Any(e => e.Id == id);
        }
    }
}
