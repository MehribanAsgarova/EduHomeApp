using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHomeApp.Models;
using EduHomeApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduHomeApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<EduHomeUser> _userManager;
        private readonly SignInManager<EduHomeUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<EduHomeUser> userManager,
                                 SignInManager<EduHomeUser> signInManager,
                                 RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);
            EduHomeUser eduHomeUser = await _userManager.FindByNameAsync(loginVM.Username);
            if (eduHomeUser == null)
            {
                ModelState.AddModelError("", "Email Or Password Is Wrong!!!");
                return View(loginVM);
            }

            if (eduHomeUser.IsDeleted)
            {
                ModelState.AddModelError("", "Your Account Is Blocked!!!");
                return View(loginVM);
            }

            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(eduHomeUser, loginVM.Password, loginVM.RememberMe, true);
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "Your Account Is Lock Out!!!");
                return View(loginVM);
            }
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Email Or Password Is Wrong!!!");
                return View(loginVM);
            }
            TempData["UserFullName"] = eduHomeUser.FullName;

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup(SignupVM signup)
        {
            if (!ModelState.IsValid) return View();

            EduHomeUser eduHomeUser = new EduHomeUser
            {
                FullName = signup.FullName,
                UserName = signup.Username,
                Email = signup.Email
            };

            IdentityResult identityResult = await _userManager.CreateAsync(eduHomeUser, signup.Password);
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            await _userManager.AddToRoleAsync(eduHomeUser, "Member");
            await _signInManager.SignInAsync(eduHomeUser, true);


            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //public async Task CreateRole()
        //{
        //    if (!await _roleManager.RoleExistsAsync("Admin"))
        //        await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });

        //    if (!await _roleManager.RoleExistsAsync("Member"))
        //        await _roleManager.CreateAsync(new IdentityRole { Name = "Member" });

        //    if (!await _roleManager.RoleExistsAsync("Teacher"))
        //        await _roleManager.CreateAsync(new IdentityRole { Name = "Teacher" });
        //}

    }
}
