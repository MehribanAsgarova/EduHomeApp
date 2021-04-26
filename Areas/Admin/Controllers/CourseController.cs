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
{   [Area("Admin")]
    public class CourseController : Controller
    {
        private readonly EduHomeDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CourseController(EduHomeDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Courses.ToList());
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Course course = await _context.Courses.Include(t => t.CourseFeature).FirstOrDefaultAsync(t => t.Id == id);

            if (course == null) return NotFound();
            return View(course);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.CourseFeatures = await _context.CourseFeatures.ToListAsync();
          
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            ViewBag.CourseFeatures = await _context.CourseFeatures.ToListAsync();

            if (!ModelState.IsValid) return View();
            //CourseFeature courseFeature = await _context.CourseFeatures.ToList();
            if (course.Photo == null)
            {
                ModelState.AddModelError("", "Choose an image");
                return View();
            }
            if (!course.Photo.IsImage())
            {
                ModelState.AddModelError("", "You must choose only img format");
                return View();
            }
            if (course.Photo.CheckFileSize(300))
            {
                ModelState.AddModelError("", "The size of image must not be more than 300Kb");
                return View();
            }

            string filefolder = Path.Combine("img", "course");
            course.ImageName = await course.Photo.SaveFileAsync(_env.WebRootPath, filefolder);
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            Course course = await _context.Courses.FindAsync(id);

            if (course == null) return NotFound();

            return View(course);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteCourse(int? id)
        {
            if (id == null) return NotFound();
            Course course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();

            Helper.DeleteFile(_env.WebRootPath, "img", course.ImageName);

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.CourseFeatures = await _context.CourseFeatures.ToListAsync();
            if (id == null) return NotFound();
            Course course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();
            return View(course);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Course course)
        {
            ViewBag.CourseFeatures = await _context.CourseFeatures.ToListAsync();
            if (id == null) return NotFound();
            Course dbCourse = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();

            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Zehmet Olmasa Shekil Secin");
                return View();
            }

            if (!course.Photo.IsImage())
            {
                ModelState.AddModelError("File", "Duzgun File Secin");
                return View();
            }

            if (course.Photo.CheckFileSize(300))
            {
                ModelState.AddModelError("Photo", "300 kb -da artiq olcude fayl yuklemek olmaz");
                return View();
            }

            Helper.DeleteFile(_env.WebRootPath, "img/course", dbCourse.ImageName);

            dbCourse.ImageName = await course.Photo.SaveFileAsync(_env.WebRootPath, "img/course");
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
