using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Lib;
using Ngo.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace Ngo.Web.Controllers
{
    [Authorize]
    public class MembersController : Controller
    {
        private readonly DataDbContext _context;

        public MembersController(DataDbContext context)=> _context = context;

        public async Task<IActionResult> Index() => View(await _context.Members.ToListAsync());
      

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

      
        public IActionResult Create()=> View();


      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MemberNo,FullName,ContactNo,Email,NationalNo,DateOfBirth,Gender,PermanentAddress,PresentAddress,FatherName")] MemberModel member)
        {
            if (ModelState.IsValid)
            {

                var model = new Member
                {
                    MemberNo = member.MemberNo,
                    NationalNo = member.NationalNo,
                    DateOfBirth = member.DateOfBirth,
                    Email = member.Email,
                    FullName = member.FullName,
                    ContactNo = member.ContactNo,
                    Gender = member.ContactNo,
                    FatherName = member.FatherName,
                    PermanentAddress = member.PermanentAddress,
                    PresentAddress = member.PresentAddress
                };

                _context.Members.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

      
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            var model = new MemberModel
            {
                MemberNo = member.MemberNo,
                NationalNo = member.NationalNo,
                DateOfBirth = member.DateOfBirth,
                Email = member.Email,
                FullName = member.FullName,
                ContactNo = member.ContactNo,
                Gender = member.ContactNo,
                FatherName = member.FatherName,
                PermanentAddress = member.PermanentAddress,
                PresentAddress = member.PresentAddress
            };
            return View(model);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MemberNo,FullName,ContactNo,Email,NationalNo,DateOfBirth,Gender,PermanentAddress,PresentAddress,FatherName")] MemberModel member)
        {
            if (id != member.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var find = _context.Members.Find(member.Id);
                    find.MemberNo = member.MemberNo;
                    find.NationalNo = member.NationalNo;
                    find.DateOfBirth = member.DateOfBirth;
                    find.Email = member.Email;
                    find.FullName = member.FullName;
                    find.ContactNo = member.ContactNo;
                    find.Gender = member.ContactNo;
                    find.FatherName = member.FatherName;
                    find.PermanentAddress = member.PermanentAddress;
                    find.PresentAddress = member.PresentAddress;
                      _context.Update(find);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.Id))
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
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _context.Members.FindAsync(id);
            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.Id == id);
        }



        [Route("api/members")]
        [HttpGet]
        public IActionResult Get()=> Json(_context.Members);
     

    }
}
