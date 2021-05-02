using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHomeApp.DAL;
using EduHomeApp.Models;
using EduHomeApp.ViewModels;
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactComment(ContactCommentVM commentCommentVM)
        {
            ContactComment contactComment = new ContactComment()
            {
                Contact = _context.Contacts.Find(commentCommentVM.id),
                Name = commentCommentVM.name,
                Email = commentCommentVM.email,
                Subject = commentCommentVM.subject,
                Description = commentCommentVM.message
            };

            await _context.ContactComments.AddAsync(contactComment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Home","Index");

        }
    }
}
