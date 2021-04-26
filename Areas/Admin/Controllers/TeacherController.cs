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
    public class TeacherController : Controller
    {
        private readonly EduHomeDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeacherController(EduHomeDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Teachers.ToList());
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Teacher teacher = await _context.Teachers.Include(t=>t.Skill).FirstOrDefaultAsync(t=>t.Id==id);

            if (teacher == null) return NotFound();
            return View(teacher);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Teacher teacher)
        {
            if (!ModelState.IsValid) return View();
           
            if (teacher.Photo == null)
            {
                ModelState.AddModelError("", "Choose an image");
                return View();
            }
            if (!teacher.Photo.IsImage())
            {
                ModelState.AddModelError("", "You must choose only img format");
                return View();
            }
            if (teacher.Photo.CheckFileSize(300))
            {
                ModelState.AddModelError("", "The size of image must not be more than 300Kb");
                return View();
            }

            string filefolder = Path.Combine("img", "teacher");
            teacher.ImageName = await teacher.Photo.SaveFileAsync(_env.WebRootPath, filefolder);

            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            Teacher teacher = await _context.Teachers.Include(t => t.Skill).FirstOrDefaultAsync(t => t.Id == id);

            if (teacher == null) return NotFound();

            return View(teacher);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteContact(int? id)
        {
            if (id == null) return NotFound();
            Teacher teacher = await _context.Teachers.Include(t => t.Skill).FirstOrDefaultAsync(t => t.Id == id);
            if (teacher == null) return NotFound();

            Helper.DeleteFile(_env.WebRootPath, "img", teacher.ImageName);

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            Teacher teacher = await _context.Teachers.Include(t => t.Skill).FirstOrDefaultAsync(t => t.Id == id);
            if (teacher == null) return NotFound();
            return View(teacher);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Teacher teacher)
        {
            if (id == null) return NotFound();
            Teacher dbTeacher = await _context.Teachers.Include(t => t.Skill).FirstOrDefaultAsync(t => t.Id == id);
            if (teacher == null) return NotFound();

            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Zehmet Olmasa Shekil Secin");
                return View();
            }

            if (!teacher.Photo.IsImage())
            {
                ModelState.AddModelError("File", "Duzgun File Secin");
                return View();
            }

            if (teacher.Photo.CheckFileSize(300))
            {
                ModelState.AddModelError("Photo", "300 kb -da artiq olcude fayl yuklemek olmaz");
                return View();
            }

            Helper.DeleteFile(_env.WebRootPath, "img/teacher", dbTeacher.Name);

            dbTeacher.Name = await teacher.Photo.SaveFileAsync(_env.WebRootPath, "img/contact");
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
