using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.Lib;
using Data.Lib.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ngo.Web.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ngo.Web.Controllers
{
    [Authorize]
    public class LoanController : Controller
    {
        private readonly DataDbContext _context;
        private readonly IMapper _mapper;
        public LoanController(DataDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Detail(int ? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(new Loan { AccountId = (int)id });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LoanAmount,LoanDate,AccountId")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loan);
                await _context.SaveChangesAsync();
                return RedirectToAction("Detail", new { id = loan.AccountId });
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id", loan.AccountId);
            return View(loan);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LoanAmount,LoanDate,AccountId")] Loan loan)
        {
            if (id != loan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepositExists(loan.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Detail", new { id = loan.AccountId });
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id", loan.AccountId);
            return View(loan);
        }

        private bool DepositExists(int id)
        {
            return _context.Loan.Any(e => e.Id == id);
        }
        [HttpGet("api/loans/{id}")]
        public IActionResult Get(int id)
        {
            var model = _context.Accounts.Include(f => f.Member).Include(f => f.Book).Include(f => f.Loan).SingleOrDefault(f => f.Id == id);

            LoanModel loan = new LoanModel
            {
                AccountId = model.Id,
                AccountNo = model.AccountNo,
                ProvideLoanList = model.Loan.Select(s => new ProvideLoanList
                {
                    Id = s.Id,
                    LoanAmount = s.LoanAmount,
                    LoanDate = s.LoanDate
                }).ToList(),
                Book = _mapper.Map<BookModel>(model.Book),
                Member = _mapper.Map<MemberModel>(model.Member)

            };
            return Json(loan);
        }
        [HttpGet("api/loancollcetion/{id}")]
        public IActionResult GetLoanCollection(int id)
        {
            var loan = _context.LoanCollection.Where(f => f.LoanId == id).ToList();

            return Json(loan);
        }
        [HttpPost("api/loancollcetion")]
        public IActionResult GetLoanCollection([FromBody]LoanCollection loan)
        {
            _context.LoanCollection.Add(loan);
            _context.SaveChanges();
             return Json(loan);
        }

        [HttpPost("api/putoancollcetion")]
        public IActionResult PutLoanCollection([FromBody]LoanCollection loan)
        {
            var model=_context.LoanCollection.Find(loan.Id);
            model.LoanCollectAmount = loan.LoanCollectAmount;
            model.LoanCollectDate = loan.LoanCollectDate;
            _context.SaveChanges();
            return Json(loan);
        }

        // GET: Deposits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan
                .Include(d => d.Account).ThenInclude(f => f.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // POST: Deposits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loan = await _context.Loan.FindAsync(id);
            _context.Loan.Remove(loan);
            await _context.SaveChangesAsync();
            return RedirectToAction("Detail", new { id = loan.AccountId });
        }
    }
}
