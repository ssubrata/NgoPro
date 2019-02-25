using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Data.Lib;
using Microsoft.AspNetCore.Mvc;
using Ngo.Web.Models;

namespace Ngo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataDbContext _context;
        public HomeController(DataDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            List<Member> members = null;

           

            _context.SaveChanges();
            members = _context.Members.ToList();

            return View(members);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
