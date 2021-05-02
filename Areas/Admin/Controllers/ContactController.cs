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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace EduHomeApp.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ContactController : Controller
    {
        private readonly EduHomeDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ContactController(EduHomeDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Contacts.ToList());
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Contact contact = await _context.Contacts.FindAsync(id);

            if (contact == null) return NotFound();
            return View(contact);
        }
            public IActionResult Create()
            {

                return View();
            }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact contact)
        {
           if (!ModelState.IsValid) return View();
            if (await _context.Contacts.AnyAsync(c => c.Description.ToLower() == contact.Description.ToLower()))
            {
                ModelState.AddModelError("Description", "This adress already exist");
                return View();
            }
            if (contact.Photo == null)
            {
                ModelState.AddModelError("", "Choose an image");
                return View();
            }
            if (!contact.Photo.IsImage())
            {
                ModelState.AddModelError("", "You must choose only img format");
                return View();
            }
            if (contact.Photo.CheckFileSize(300))
            {
                ModelState.AddModelError("", "The size of image must not be more than 300Kb");
                return View();
            }
            
            string filefolder = Path.Combine("img", "contact");
            contact.Name = await contact.Photo.SaveFileAsync(_env.WebRootPath, filefolder);
        
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
            }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            Contact contact = await _context.Contacts.FindAsync(id);

            if (contact == null) return NotFound();

            return View(contact);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteContact(int? id)
        {
            if (id == null) return NotFound();
            Contact contact = await _context.Contacts.FindAsync(id);
            if (contact == null) return NotFound();

            Helper.DeleteFile(_env.WebRootPath, "img", contact.Name);

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            Contact contact = await _context.Contacts.FindAsync(id);
            if (contact == null) return NotFound();
            return View(contact);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Contact contact)
        {
            if (id == null) return NotFound();
            Contact dbContact = await _context.Contacts.FindAsync(id);
            if (contact == null) return NotFound();

            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photo", "Zehmet Olmasa Shekil Secin");
                return View();
            }

            if (!contact.Photo.IsImage())
            {
                ModelState.AddModelError("File", "Duzgun File Secin");
                return View();
            }

            if (contact.Photo.CheckFileSize(300))
            {
                ModelState.AddModelError("Photo", "300 kb -da artiq olcude fayl yuklemek olmaz");
                return View();
            }

            Helper.DeleteFile(_env.WebRootPath, "img/contact",dbContact.Name);

            dbContact.Name = await contact.Photo.SaveFileAsync(_env.WebRootPath, "img/contact");
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
