using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthStore.Data;
using AuthStore.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace AuthStore.Controllers
{
    public class AdsController : Controller
    {

        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment webHostEnvironment;

        public AdsController(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            webHostEnvironment = webHost;
        }

        // GET: Ads
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ads.ToListAsync());
        }

        // GET: Ads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ads = await _context.Ads
                .FirstOrDefaultAsync(m => m.AdsId == id);
            if (ads == null)
            {
                return NotFound();
            }

            return View(ads);
        }

        // GET: Ads/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        public IActionResult Create(Ads ads)
        {
            string uniqueFileName = UploadedFile(ads);
            ads.ImageUrl = uniqueFileName;
            _context.Attach(ads);
            _context.Entry(ads).State = EntityState.Added;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        private string UploadedFile(Ads ads)
        {
            string uniqueFileName = null;
            if (ads.FrontImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "ImgAds");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + ads.FrontImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ads.FrontImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }


        // GET: Ads/Edit/5
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            
            Ads ads = _context.Ads.Where(a => a.AdsId == id).FirstOrDefault();
            return View(ads);
        }

        [HttpPost]


        public IActionResult Edit(Ads ads)
        {
            string uniqueFileName = UploadedFile(ads);
            ads.ImageUrl = uniqueFileName;
            _context.Attach(ads);
            _context.Entry(ads).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        // GET: Ads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ads = await _context.Ads
                .FirstOrDefaultAsync(m => m.AdsId == id);
            if (ads == null)
            {
                return NotFound();
            }

            return View(ads);
        }

        // POST: Ads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ads = await _context.Ads.FindAsync(id);
            _context.Ads.Remove(ads);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdsExists(int id)
        {
            return _context.Ads.Any(e => e.AdsId == id);
        }
    }
}
