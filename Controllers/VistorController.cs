using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthStore.Data;
using AuthStore.Models;

namespace AuthStore.Controllers
{
    public class VistorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VistorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vistor
        public async Task<IActionResult> Index(int? id)
        {
            var r = (from Itms in _context.Products where Itms.ProductId == id select Itms).ToList();
            //Counter + 1
            var v = _context.Vistors.Find(1);
            v.Number++;
            _context.SaveChanges();

            // Select Counter
            var v1 = _context.Vistors.Find(1);
            ViewBag.vists = v1.Number;

            //Sum of cat.
            var ca = (from c in _context.Categories select c).ToList();
            ViewBag.cat_count = ca.Count;

            //Sum of Itms.
            var itm = (from c in _context.Products select c).ToList();
            ViewBag.itm_count = itm.Count;

            return View(await _context.Vistors.ToListAsync());
        }

        // GET: Vistor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vistor = await _context.Vistors
                .FirstOrDefaultAsync(m => m.VistId == id);
            if (vistor == null)
            {
                return NotFound();
            }

            return View(vistor);
        }

        // GET: Vistor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vistor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VistId,Number")] Vistor vistor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vistor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vistor);
        }

        // GET: Vistor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vistor = await _context.Vistors.FindAsync(id);
            if (vistor == null)
            {
                return NotFound();
            }
            return View(vistor);
        }

        // POST: Vistor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VistId,Number")] Vistor vistor)
        {
            if (id != vistor.VistId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vistor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VistorExists(vistor.VistId))
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
            return View(vistor);
        }

        // GET: Vistor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vistor = await _context.Vistors
                .FirstOrDefaultAsync(m => m.VistId == id);
            if (vistor == null)
            {
                return NotFound();
            }

            return View(vistor);
        }

        // POST: Vistor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vistor = await _context.Vistors.FindAsync(id);
            _context.Vistors.Remove(vistor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VistorExists(int id)
        {
            return _context.Vistors.Any(e => e.VistId == id);
        }
    }
}
