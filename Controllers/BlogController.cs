using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHomeApp.DAL;
using EduHomeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHomeApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly EduHomeDbContext _context;

        public BlogController(EduHomeDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? page)
        {

            ViewBag.PageCount = Math.Ceiling((decimal)_context.Blogs.Count() / 9);
            if (page == null)
            {
                //po umolchaniyu stranica 1
                ViewBag.Page = 1;

                return View(await _context.Blogs.OrderByDescending(p => p.Id).Take(9).ToListAsync());
            }
            else
            {
                ViewBag.Page = page;

                return View(await _context.Blogs.OrderByDescending(p => p.Id).Skip(((int)page - 1) * 9).Take(9).ToListAsync());
            }
            //return View(_context.Blogs.ToList());
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();

            Blog blog = await _context.Blogs.FindAsync(id);
            if (blog == null) return NotFound();

            return View(blog);
        }
        public async Task<IActionResult> Search(string? key)
        {
            List<Blog> blogs = await _context.Blogs.Where(b => b.Title.Contains(key)).ToListAsync();

            return PartialView("~/Views/Shared/Partials/_BlogPartial.cshtml", blogs);
        }
    }
}
