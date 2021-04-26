using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EduHomeApp.DAL;
using EduHomeApp.Extensions;
using EduHomeApp.Helpers;
using EduHomeApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace EduHomeApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly EduHomeDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BlogController(EduHomeDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Blogs.ToList());
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Blog blog = await _context.Blogs.FindAsync(id);

            if (blog == null) return NotFound();
            return View(blog);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (!ModelState.IsValid) return View();
          
            if (blog.Photo == null)
            {
                ModelState.AddModelError("", "Choose an image");
                return View();
            }
            if (!blog.Photo.IsImage())
            {
                ModelState.AddModelError("", "You must choose only img format");
                return View();
            }
            if (blog.Photo.CheckFileSize(300))
            {
                ModelState.AddModelError("", "The size of image must not be more than 300Kb");
                return View();
            }

            string filefolder = Path.Combine("img", "blog");
            blog.Name = await blog.Photo.SaveFileAsync(_env.WebRootPath, filefolder);

            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            Blog blog = await _context.Blogs.FindAsync(id);

            if (blog == null) return NotFound();

            return View(blog);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteBlog(int? id)
        {
            if (id == null) return NotFound();
            Blog blog = await _context.Blogs.FindAsync(id);
            if (blog == null) return NotFound();

            Helper.DeleteFile(_env.WebRootPath, "img", blog.Name);

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            Blog blog = await _context.Blogs.FindAsync(id);
            if (blog == null) return NotFound();
            return View(blog);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Blog blog)
        {
            if (id == null) return NotFound();
            Blog dbBlog = await _context.Blogs.FindAsync(id);
            if (blog == null) return NotFound();

            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Zehmet Olmasa Shekil Secin");
                return View();
            }

            if (!blog.Photo.IsImage())
            {
                ModelState.AddModelError("File", "Duzgun File Secin");
                return View();
            }

            if (blog.Photo.CheckFileSize(300))
            {
                ModelState.AddModelError("Photo", "300 kb -da artiq olcude fayl yuklemek olmaz");
                return View();
            }

            Helper.DeleteFile(_env.WebRootPath, "img/blog", dbBlog.Name);

            dbBlog.Name = await blog.Photo.SaveFileAsync(_env.WebRootPath, "img/blog");
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
