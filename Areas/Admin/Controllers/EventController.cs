using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EduHomeApp.DAL;
using EduHomeApp.Extensions;
using EduHomeApp.Helpers;
using EduHomeApp.Models;
using EduHomeApp.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpeakerEventVM speakerEventVM)
        {
           
            ViewBag.Speaker = await _context.Speakers.ToListAsync();
            if (!ModelState.IsValid) return View();

            if (speakerEventVM.Event.Photo == null)
            {
                ModelState.AddModelError("", "Choose an image");
                return View();
            }
            if (!speakerEventVM.Event.Photo.IsImage())
            {
                ModelState.AddModelError("", "You must choose only img format");
                return View();
            }
            if (speakerEventVM.Event.Photo.CheckFileSize(300))
            {
                ModelState.AddModelError("", "The size of image must not be more than 300Kb");
                return View();
            }

            string filefolder = Path.Combine("img", "event");

            string filename = await speakerEventVM.Event.Photo.SaveFileAsync(_env.WebRootPath, filefolder);

            Event @event = new Event()
            {
                ImageName = filename,
                Title=speakerEventVM.Event.Title,
                Date = speakerEventVM.Event.Date,
                StartTime = speakerEventVM.Event.StartTime,
                EndTime = speakerEventVM.Event.EndTime,
                Location = speakerEventVM.Event.Location,
                Description = speakerEventVM.Event.Description
            };

            ICollection<SpeakerEvent> speakerEvents = new List<SpeakerEvent>();

            foreach (int id in speakerEventVM.speakerIds)
            {
                speakerEvents.Add(new SpeakerEvent() { SpeakerId = id });
            }

            @event.SpeakerEvent = speakerEvents;

            await _context.Events.AddAsync(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            Event @event = await _context.Events.FindAsync(id);

            if (@event == null) return NotFound();

            return View(@event);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteEvent(int? id)
        {
            if (id == null) return NotFound();
            Event @event = await _context.Events.FindAsync(id);
            if (@event == null) return NotFound();

            Helper.DeleteFile(_env.WebRootPath, "img", @event.ImageName);

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Speaker = await _context.Speakers.ToListAsync();
            if (id == null) return NotFound();
            Event @event = await _context.Events.FindAsync(id);
            if (@event == null) return NotFound();
            //SpeakerEventVM speaker = new SpeakerEventVM()
            //{
            //    Event = @event
            //};
            
            return View(@event);
        }
      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Event formevent, int[] speakerIds)
        {
            ViewBag.Speaker = await _context.Speakers.ToListAsync();
            if (id == null) return NotFound();

            //speakerEventVM.Event.Id = (int)id;
            //_context.Update(speakerEventVM.Event);
            Event @event = await _context.Events.Include(e=>e.SpeakerEvent).FirstOrDefaultAsync(e=>e.Id == id);
            if (@event == null) return NotFound();

            @event.Title = formevent.Title;
            @event.Description = formevent.Description;
            @event.Date = formevent.Date;
            @event.StartTime = formevent.StartTime;
            @event.EndTime = formevent.EndTime;
            @event.Location = formevent.Location;
            foreach (int SEid in speakerIds)
            {
                if (!@event.SpeakerEvent.Any(s=>s.SpeakerId == SEid))
                {
                    @event.SpeakerEvent.Add(new SpeakerEvent() { SpeakerId = SEid });
                }
                
            }
            string filefolder = Path.Combine("img", "event");

            string filename = await formevent.Photo.SaveFileAsync(_env.WebRootPath, filefolder);
            @event.ImageName = filename;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
           
        }
        

    }
}
