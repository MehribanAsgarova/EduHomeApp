using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHomeApp.DAL;
using EduHomeApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EduHomeApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly EduHomeDbContext _context;

        public HomeController(EduHomeDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View(new HomeVM
            {
                Slides = _context.Slides,
                Notices=_context.Notices,
                
            });
        }
    }
}
