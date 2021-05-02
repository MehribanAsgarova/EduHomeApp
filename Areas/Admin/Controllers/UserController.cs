using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHomeApp.DAL;
using EduHomeApp.Models;
using EduHomeApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduHomeApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<EduHomeUser> _userManager;
        private readonly EduHomeDbContext _context;
        public UserController(UserManager<EduHomeUser> userManager, EduHomeDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<EduHomeUser> eduHomeUsers = _userManager.Users.ToList();
            List<UserVM> userVMs = new List<UserVM>();
            foreach (EduHomeUser eduHomeUser in eduHomeUsers)
            {
                UserVM userVM = new UserVM
                {
                    Id = eduHomeUser.Id,
                    FullName = eduHomeUser.FullName,
                    Email = eduHomeUser.Email,
                    Username = eduHomeUser.UserName,
                    IsDeleted = eduHomeUser.IsDeleted,
                    Role = (await _userManager.GetRolesAsync(eduHomeUser))[0]
                };
                userVMs.Add(userVM);
            }
            return View(userVMs);
        }
        public async Task<IActionResult> IsActive(string id)
        {
            if (id == null) return NotFound();
            EduHomeUser eduHomeUser = await _userManager.FindByIdAsync(id);
            if (eduHomeUser == null) return NotFound();
            UserVM userVM = new UserVM
            {
                Id = eduHomeUser.Id,
                FullName = eduHomeUser.FullName,
                Email = eduHomeUser.Email,
                Username = eduHomeUser.UserName,
                Role = (await _userManager.GetRolesAsync(eduHomeUser))[0],
                IsDeleted = eduHomeUser.IsDeleted
            };
            return View(userVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IsActive(string id, bool IsActivated)
        {
            if (id == null) return NotFound();
            EduHomeUser eduHomeUser = await _userManager.FindByIdAsync(id);
            if (eduHomeUser == null) return NotFound();
            eduHomeUser.IsDeleted = IsActivated;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ChangeRole(string id)
        {
            if (id == null) return NotFound();
            EduHomeUser eduHomeUser = await _userManager.FindByIdAsync(id);
            if (eduHomeUser == null) return NotFound();
            UserVM userVM = new UserVM
            {
                Id = eduHomeUser.Id,
                FullName = eduHomeUser.FullName,
                Email = eduHomeUser.Email,
                Username = eduHomeUser.UserName,
                Role = (await _userManager.GetRolesAsync(eduHomeUser))[0],
                IsDeleted = eduHomeUser.IsDeleted,
                Roles = new List<string> { "Admin", "Member", "Teacher" }
            };
            return View(userVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeRole(string id, string Role)
        {
            if (id == null) return NotFound();
            EduHomeUser eduHomeUser = await _userManager.FindByIdAsync(id);
            if (eduHomeUser == null) return NotFound();
            string oldRole = (await _userManager.GetRolesAsync(eduHomeUser))[0];
            await _userManager.RemoveFromRoleAsync(eduHomeUser, oldRole);
            await _userManager.AddToRoleAsync(eduHomeUser, Role);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ChangePassword(string id)
        {
            if (id == null) return NotFound();
            EduHomeUser eduHomeUser = await _userManager.FindByIdAsync(id);
            if (eduHomeUser == null) return NotFound();
           
            UserVM userVM = new UserVM
            {
                Id = eduHomeUser.Id,
                FullName = eduHomeUser.FullName,
                Email = eduHomeUser.Email,
                Username = eduHomeUser.UserName,
                Role = (await _userManager.GetRolesAsync(eduHomeUser))[0],
                IsDeleted = eduHomeUser.IsDeleted,

                Password = eduHomeUser.PasswordHash.GetHashCode().ToString()
            };
            return View(userVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string id, string NewPassword)
        {
            if (id == null) return NotFound();
            EduHomeUser eduHomeUser = await _userManager.FindByIdAsync(id);
            if (eduHomeUser == null) return NotFound();
            string token = await _userManager.GeneratePasswordResetTokenAsync(eduHomeUser);
            await _userManager.ResetPasswordAsync(eduHomeUser, token, NewPassword);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SignupVM signup)
        {
          
            EduHomeUser eduHomeUser = new EduHomeUser
            {
                FullName = signup.FullName,
                UserName = signup.Username,
                Email = signup.Email
            };

            IdentityResult identityResult = await _userManager.CreateAsync(eduHomeUser, signup.Password);
            await _userManager.AddToRoleAsync(eduHomeUser, "Member");
           // await _signInManager.SignInAsync(eduHomeUser, true);

           
            return RedirectToAction(nameof(Index));

        }
    }
}
