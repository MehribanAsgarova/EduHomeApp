using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using EduHomeApp.DAL;
using EduHomeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHomeApp.Controllers
{
    public class EventController : Controller
    {
        private readonly EduHomeDbContext _context;

        public EventController(EduHomeDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //string date = DateTime.Now.ToString(@"hh:mm tt", new CultureInfo("en-US"));

            return View(_context.Events.ToList());
            
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            
            Event event1 = await _context.Events.Include(e => e.SpeakerEvent).ThenInclude(sp => sp.Speaker).FirstOrDefaultAsync(e => e.Id == id);
            if (event1 == null) return NotFound();
            return View(event1);
           
        }
        public async Task<IActionResult> Search(string? key)
        {
            List<Event> events = await _context.Events.Where(t => t.Title.Contains(key)).ToListAsync();

            return PartialView("~/Views/Shared/Partials/_EventPartial.cshtml", events);
        }
    }
}
