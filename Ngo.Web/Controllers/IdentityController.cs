using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Lib.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ngo.Web.Models;

namespace Ngo.Web.Controllers
{
    public class IdentityController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityController(
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;

        }
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [Authorize]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("UserName", "Password")] LoginModel loginModel, string returnUrl)
        {
            if (loginModel == null) return NotFound();
            if (ModelState.IsValid)
            {
                var user=new AppUser { UserName = loginModel.UserName, PasswordHash = loginModel.Password };

                var result = await _signInManager.PasswordSignInAsync(user.UserName, user.PasswordHash, false, false);
                if (result.Succeeded)
                {
                    return Redirect(returnUrl);
                }
                
            }

            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserName","Password")] LoginModel loginModel)
        {
            if (loginModel == null) return NotFound();
            if (ModelState.IsValid)
            {
              IdentityResult result= await  _userManager.CreateAsync(new AppUser { UserName = loginModel.UserName, PasswordHash = loginModel.Password },loginModel.Password);
                if(result.Succeeded)
                {
                    return View("Login");
                }
            }
            
            return View(ModelState);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
           await _signInManager.SignOutAsync();
            return View("Login");
        }
    }
}