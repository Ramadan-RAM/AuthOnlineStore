using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AuthStore.Data;
using AuthStore.Models;
using AuthStore.Utility;
using X.PagedList;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AuthStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
       
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
          
        }

        public IActionResult Chat()
        {
            return View();
        }
      
        public IActionResult Index(string SearchByName)
        {
            List<Product> products = _context.Products.ToList();

            //ViewBag.Category = GetCategories();
            //ViewBag.Product = GetProducts();

            //var products = GetProducts();
            if (!string.IsNullOrEmpty(SearchByName))

            products = products.Where(e => e.ProductName.ToLower().Contains(SearchByName.ToLower())).ToList();
            return View(nameof(Index), products);
        }

        // GET product detail acation method
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = _context.Products.Include(c => c.Category).FirstOrDefault(c => c.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //POST product detail acation method
        [HttpPost]
        [ActionName("Detail")]
        public ActionResult ProductDetail(int? id)
        {
            List<Product> products = new List<Product>();
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products.Include(c => c.Category).FirstOrDefault(c => c.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            products = HttpContext.Session.Get<List<Product>>("products");
            if (products == null)
            {
                products = new List<Product>();
            }
            products.Add(product);
            HttpContext.Session.Set("products", products);
            return RedirectToAction(nameof(Index));
        }

        //GET Remove action methdo
        [ActionName("Remove")]
        public IActionResult RemoveToCart(int? id)
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.ProductId == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Remove(int? id)
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.ProductId == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        //GET product Cart action method

        public IActionResult Cart()
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");
            if (products == null)
            {
                products = new List<Product>();
            }
            return View(products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<Product> GetProducts()
        {
            var product = (from pro in _context.Products
                            join category in _context.Categories on pro.CategorId equals category.CategorId
                            select new Product
                            {
                                ProductId = pro.ProductId,
                                ProductName = pro.ProductName,
                                ProductCode = pro.ProductCode,
                                Price = pro.Price,
                                ImageUrl = pro.ImageUrl,
                                Description = pro.Description,
                                FrontImage = pro.FrontImage,
                                Cost = pro.Cost,
                                Quantity = pro.Quantity,
                                CategorId = pro.CategorId,
                                CategoryName = category.CategoryName

                            }).ToList();

            return product;
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
    }
}
