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
    public class CourseController : Controller
    {
        private readonly EduHomeDbContext _context;

        public CourseController(EduHomeDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Courses.ToList());
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Course course = await _context.Courses.Include(c => c.CourseFeature).FirstOrDefaultAsync(c => c.Id == id);
            if (course == null) return NotFound();
            
            return View(course);

        }
        public async Task<IActionResult> Search(string? key)
        {
            List<Course> courses = await _context.Courses.Where(c => c.CourseName.Contains(key)).ToListAsync();

            return PartialView("~/Views/Shared/Partials/_CoursePartial.cshtml", courses);
        }
    }
}

