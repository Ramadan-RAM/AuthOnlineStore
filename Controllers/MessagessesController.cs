using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthStore.Data;
using AuthStore.Models;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace AuthStore.Controllers
{
    [Authorize]
    public class MessagessesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<AppUser> _userManger;
        //, UserManager<AppUser> userManager

        public MessagessesController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManger = userManager;
        }

        public async Task<IActionResult> Index()
        {
            AppUser currentUser = await _userManger.GetUserAsync(User);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentUserName = currentUser.UserName;
            }
            var messagess = await _context.Messagess.ToListAsync();
            return View(messagess);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Messagess messagess)
        {
            if (ModelState.IsValid)
            {
                messagess.UserName = User.Identity.Name;
                var sender = await _userManger.GetUserAsync(User);
                messagess.UserID = sender.Id;
                await _context.Messagess.AddAsync(messagess);
                await _context.SaveChangesAsync();
                return Ok();
            }

            return Error();
        }


        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.Messagess.Include(m => m.Sender);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        //public IActionResult Create()
        //{
        //    ViewData["UserID"] = new SelectList(_context.Set<AppUser>(), "Id", "Id");
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,UserName,Text,When,UserID")] Messagess messagess)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(messagess);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["UserID"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", messagess.UserID);
        //    return View(messagess);
        //}

        // GET: Messagesses

        // GET: Messagesses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messagess = await _context.Messagess
                .Include(m => m.Sender)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messagess == null)
            {
                return NotFound();
            }

            return View(messagess);
        }

       

        // GET: Messagesses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messagess = await _context.Messagess.FindAsync(id);
            if (messagess == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", messagess.UserID);
            return View(messagess);
        }

        // POST: Messagesses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Text,When,UserID")] Messagess messagess)
        {
            if (id != messagess.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(messagess);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessagessExists(messagess.Id))
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
            ViewData["UserID"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", messagess.UserID);
            return View(messagess);
        }

        // GET: Messagesses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messagess = await _context.Messagess
                .Include(m => m.Sender)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messagess == null)
            {
                return NotFound();
            }

            return View(messagess);
        }

        // POST: Messagesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var messagess = await _context.Messagess.FindAsync(id);
            _context.Messagess.Remove(messagess);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessagessExists(int id)
        {
            return _context.Messagess.Any(e => e.Id == id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
