using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHomeApp.DAL;
using EduHomeApp.Models;
using EduHomeApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduHomeApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly EduHomeDbContext _context;
        private readonly UserManager<EduHomeUser> _userManager;

        public HomeController(EduHomeDbContext context, UserManager<EduHomeUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            TempData["UserFullName"] = "";

            if (User.Identity.IsAuthenticated)
            {
                EduHomeUser eduHomeUser = await _userManager.FindByNameAsync(User.Identity.Name);

                TempData["UserFullName"] = eduHomeUser.FullName;
            }

            return View(new HomeVM
            {
                Slides = _context.Slides,
                Notices=_context.Notices,
                InfoBoards=_context.InfoBoards,
                Courses=_context.Courses,
                Events=_context.Events,
                Blogs=_context.Blogs,
                
            });
        }
    }
}
