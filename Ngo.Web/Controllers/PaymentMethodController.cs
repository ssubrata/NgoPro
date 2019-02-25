using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Lib;
using Data.Lib.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ngo.Web.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ngo.Web.Controllers
{
    [Authorize]
    public class PaymentMethodController : Controller
    {
        private readonly DataDbContext _context;

        public PaymentMethodController(DataDbContext context) => _context = context;
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,PaymentMethodName")] PaymentMethodModel model)
        {
            if (ModelState.IsValid)
            {
                var paymentMethod = new PaymentMethod
                {
                    PaymentMethodName = model.PaymentMethodName
                };
                _context.PaymentMethods.Add(paymentMethod);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var findMethod = _context.PaymentMethods.Find(id);
            if (findMethod == null) return NotFound();
            var model = new PaymentMethodModel
            {
                Id = findMethod.Id,
                PaymentMethodName = findMethod.PaymentMethodName
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,PaymentMethodName")] PaymentMethodModel model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var find = _context.PaymentMethods.Find(model.Id);
                    find.PaymentMethodName = model.PaymentMethodName;
                    _context.SaveChanges();


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MethodExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentMethod = await _context.PaymentMethods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentMethod == null)
            {
                return NotFound();
            }

            return View(paymentMethod);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentMethod = await _context.PaymentMethods.FindAsync(id);
            _context.PaymentMethods.Remove(paymentMethod);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MethodExists(int id)
        {
            return _context.PaymentMethods.Any(e => e.Id == id);
        }


        [Route("api/PaymentMethod")]
        [HttpGet]
        public IActionResult Get() => Json(_context.PaymentMethods);
    }
}
