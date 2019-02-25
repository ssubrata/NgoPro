using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Lib;
using Data.Lib.Entity;
using Microsoft.AspNetCore.Authorization;

namespace Ngo.Web.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private readonly DataDbContext _context;

        public AccountsController(DataDbContext context) => _context = context;


        [Route("api/accounts")]
        [HttpGet]
        public IActionResult Get() => Json(_context.Accounts.Include(f => f.Book).Include(f => f.Member));


        [HttpGet("api/accounts/{id}")]
        public IActionResult Get(int id) => Json(_context.Accounts.Include(f => f.Book).ThenInclude(f=>f.PaymentMethod).Include(f => f.Member).SingleOrDefault(f=>f.Id==id));

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            var dataDbContext = _context.Accounts.Include(a => a.Book).Include(a => a.Member);
            return View(await dataDbContext.ToListAsync());
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.Book)
                .Include(a => a.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "BookName");
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "FullName");
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountNo,Status,MemberId,BookId,CreateDate")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "BookName", account.BookId);
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "FullName", account.MemberId);
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "BookName", account.BookId);
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "FullName", account.MemberId);
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountNo,Status,MemberId,BookId,CreateDate")] Account account)
        {
            if (id != account.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Id))
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
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "BookName", account.BookId);
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "FullName", account.MemberId);
            return View(account);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.Book)
                .Include(a => a.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
    }
}
