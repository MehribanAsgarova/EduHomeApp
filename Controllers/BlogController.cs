using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EduHomeApp.DAL;
using EduHomeApp.Models;
using EduHomeApp.ViewModels;
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
                return View(await _context.Blogs.Include(b=>b.BlogComment).OrderByDescending(p => p.Id).Take(9).ToListAsync());
            }
            else
            {
                ViewBag.Page = page;

                return View(await _context.Blogs.Include(b => b.BlogComment).OrderByDescending(p => p.Id).Skip(((int)page - 1) * 9).Take(9).ToListAsync());
            }
            //return View(_context.Blogs.ToList());
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();

            Blog blog = await _context.Blogs.Include(b=>b.BlogComment).FirstOrDefaultAsync(b=>b.Id==id);
            if (blog == null) return NotFound();

            return View(blog);
        }
        public async Task<IActionResult> Search(string? key)
        {
            List<Blog> blogs = await _context.Blogs.Where(b => b.Title.Contains(key)).ToListAsync();

            return PartialView("~/Views/Shared/Partials/_BlogPartial.cshtml", blogs);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> BlogComment(string name,string email, string subject, string message,int blogId)
        //{
        //    BlogComment blogComment = new BlogComment()
        //    {
        //        Blog = _context.Blogs.Find(blogId),
        //        Name=name,
        //        Email=email,
        //        Subject=subject,
        //        Description=message
        //    };

        //    _context.BlogComments.Add(blogComment);
        //    await _context.SaveChangesAsync();
        //    return View(blogComment);

        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BlogComment(BlogCommentVM blogCommentVM)
        {
            BlogComment blogComment = new BlogComment()
            {
                Blog = _context.Blogs.Find(blogCommentVM.id),
                Name = blogCommentVM.name,
                Email = blogCommentVM.email,
                Subject = blogCommentVM.subject,
                Description = blogCommentVM.message
            };

            await _context.BlogComments.AddAsync(blogComment);
            await _context.SaveChangesAsync();
            return View();

        }
    }
}
