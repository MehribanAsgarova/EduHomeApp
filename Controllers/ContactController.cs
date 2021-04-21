using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHomeApp.DAL;
using Microsoft.AspNetCore.Mvc;

namespace EduHomeApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly EduHomeDbContext _context;

        public ContactController(EduHomeDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Contacts.ToList());
        }
    }
}
