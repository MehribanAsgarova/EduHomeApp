using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHomeApp.DAL;
using EduHomeApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EduHomeApp.Controllers
{
    public class AboutController : Controller
    {
        private readonly EduHomeDbContext _context;

        public AboutController(EduHomeDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(new AboutVM
            {
                Abouts=_context.Abouts,
                Teachers =_context.Teachers.Take(4),
                Notices=_context.Notices,
                //Slides = _context.Slides,
                //Notices = _context.Notices,

            });
        }
    }
}
