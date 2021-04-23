using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHomeApp.DAL;
using EduHomeApp.Models;
using EduHomeApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHomeApp.Controllers
{
    public class TeacherController : Controller
    {
        private readonly EduHomeDbContext _context;

        public TeacherController(EduHomeDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            return View(_context.Teachers.ToList());
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();

            Teacher teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null) return NotFound();

            TeacherVM teacherVM = new TeacherVM
            {
                Teacher=teacher,
                Teachers = await _context.Teachers.Include(c => c.Skill).ToListAsync()
            };

            return View(teacherVM);
        }
        public async Task<IActionResult> Search(string? key)
        {
            List<Teacher> teachers = await _context.Teachers.Where(t => t.Name.Contains(key)).ToListAsync();

            return PartialView("~/Views/Shared/Partials/_TeacherPartial.cshtml", teachers);
        }
    }
}
