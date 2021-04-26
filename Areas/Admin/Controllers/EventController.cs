using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EduHomeApp.DAL;
using EduHomeApp.Extensions;
using EduHomeApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHomeApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EventController : Controller
    {
        private readonly EduHomeDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EventController(EduHomeDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Events.ToList());
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Event @event = await _context.Events.Include(e=>e.SpeakerEvent).ThenInclude(se=>se.Speaker).FirstOrDefaultAsync(t => t.Id == id);

            if (@event == null) return NotFound();
            return View(@event);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Speaker = await _context.Speakers.ToListAsync();

            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Event @event)
        //{
        //    ViewBag.CourseFeatures = await _context.CourseFeatures.ToListAsync();

        //    if (!ModelState.IsValid) return View();
        //    //CourseFeature courseFeature = await _context.CourseFeatures.ToList();
        //    if (@event.Photo == null)
        //    {
        //        ModelState.AddModelError("", "Choose an image");
        //        return View();
        //    }
        //    if (!@event.Photo.IsImage())
        //    {
        //        ModelState.AddModelError("", "You must choose only img format");
        //        return View();
        //    }
        //    if (@event.Photo.CheckFileSize(300))
        //    {
        //        ModelState.AddModelError("", "The size of image must not be more than 300Kb");
        //        return View();
        //    }

        //    string filefolder = Path.Combine("img", "event");
        //    @event.ImageName = await @event.Photo.SaveFileAsync(_env.WebRootPath, filefolder);
        //    await _context.Events.AddAsync(@event);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));

        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event @event, int? SpId)
        {
            ViewBag.CourseFeatures = await _context.CourseFeatures.ToListAsync();

            if (!ModelState.IsValid) return View();
            
            if (@event.Photo == null)
            {
                ModelState.AddModelError("", "Choose an image");
                return View();
            }
            if (!@event.Photo.IsImage())
            {
                ModelState.AddModelError("", "You must choose only img format");
                return View();
            }
            if (@event.Photo.CheckFileSize(300))
            {
                ModelState.AddModelError("", "The size of image must not be more than 300Kb");
                return View();
            }

            List<Speaker> speaker = new List<Speaker>
            {
                new Speaker{Id = (int)SpId},
                
            };

            string filefolder = Path.Combine("img", "event");
            @event.ImageName = await @event.Photo.SaveFileAsync(_env.WebRootPath, filefolder);
            
            await _context.Events.AddAsync(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

    }
}
