using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Lib;
using Data.Lib.Entity;
using Ngo.Web.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Ngo.Web.Controllers
{
    [Authorize]
    public class DepositsController : Controller
    {
        private readonly DataDbContext _context;
        private readonly IMapper _mapper;
        public DepositsController(DataDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Deposits
        public IActionResult Index()
        {
          //  var dataDbContext = _context.Deposits.Include(d => d.Account);
            return View();
        }

        [HttpGet("api/deposits/{id}")]
        public IActionResult Get(int id) {
            var model = _context.Accounts.Include(f => f.Member).Include(f => f.Book).Include(f => f.Deposit).SingleOrDefault(f => f.Id == id);

            DepositModel deposit = new DepositModel
            {
                AccountId = model.Id,
                AccountNo = model.AccountNo,
                DepositCollection = model.Deposit.Select(s => new DepositCollectionModel {
                    Id=s.Id,
                    DepositAmount=s.DepositAmount,
                    DepositDate=s.DepositDate
                }).ToList(),
                Book= _mapper.Map<BookModel>(model.Book),
                Member=_mapper.Map<MemberModel>(model.Member)
              
            };
          return Json(deposit);
        }
        // GET: Deposits/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(new Deposit { AccountId = (int)id });
        }

        // GET: Deposits/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id");
            return View();
        }

        // POST: Deposits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DepositAmount,DepositDate,AccountId")] Deposit deposit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deposit);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id=deposit.AccountId});
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id", deposit.AccountId);
            return View(deposit);
        }

        // GET: Deposits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deposit = await _context.Deposits.FindAsync(id);
            if (deposit == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id", deposit.AccountId);
            return View(deposit);
        }

        // POST: Deposits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DepositAmount,DepositDate,AccountId")] Deposit deposit)
        {
            if (id != deposit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deposit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepositExists(deposit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { id = deposit.AccountId });
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id", deposit.AccountId);
            return View(deposit);
        }

        // GET: Deposits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deposit = await _context.Deposits
                .Include(d => d.Account).ThenInclude(f=>f.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deposit == null)
            {
                return NotFound();
            }

            return View(deposit);
        }

        // POST: Deposits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deposit = await _context.Deposits.FindAsync(id);
            _context.Deposits.Remove(deposit);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = deposit.AccountId });
        }

        private bool DepositExists(int id)
        {
            return _context.Deposits.Any(e => e.Id == id);
        }
    }
}
