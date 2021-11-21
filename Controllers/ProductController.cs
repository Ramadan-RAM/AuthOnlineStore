using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AuthStore.Data;
using AuthStore.Models;
using Microsoft.AspNetCore.Authorization;

namespace AuthStore.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;

            webHostEnvironment = webHost;
        }

        public IActionResult Index()
        {
            List<Product> products = _context.Products.ToList();

            return View(products);
        }

        //public IActionResult Index()
        //{
        //    return View(_context.Products.Include(c => c.Category).ToList());
        //}

        [HttpPost]
        public IActionResult Index(decimal? lowAmount, decimal? largeAmount)
        {
            var products = _context.Products.Include(c => c.Category)
                .Where(c => c.Price >= lowAmount && c.Price <= largeAmount).ToList();
            if (lowAmount == null || largeAmount == null)
            {
                products = _context.Products.Include(c => c.Category).ToList();
            }
            return View(products);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Category = GetCategories();
            Product product = new();

            return View(product);
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            string uniqueFileName = UploadedFile(product);
            product.ImageUrl = uniqueFileName;
            _context.Attach(product);
            _context.Entry(product).State = EntityState.Added;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
           ViewBag.Category = GetCategories();
            Product product = _context.Products.Where(p => p.ProductId == id).FirstOrDefault();
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            string uniqueFileName = UploadedFile(product);
            product.ImageUrl = uniqueFileName;
            _context.Attach(product);
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        private List<SelectListItem> GetCategories()
        {
            var lstCategories = new List<SelectListItem>();

            lstCategories = _context.Categories.Select(ct => new SelectListItem()
            {
                Value = ct.CategorId.ToString(),
                Text = ct.CategoryName
            }).ToList();

            var Item = new SelectListItem()
            {
                Value = null,
                Text = "---Select Category---"
            };

            lstCategories.Insert(0, Item);

            return lstCategories;
        }

        private string UploadedFile(Product product)
        {
            string uniqueFileName = null;
            if (product.FrontImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + product.FrontImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                product.FrontImage.CopyTo(fileStream);
            }
            return uniqueFileName;
        }

     

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
