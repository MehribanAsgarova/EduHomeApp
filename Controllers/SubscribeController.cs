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
    
    public class SubscribeController : Controller
    {
        private readonly EduHomeDbContext _context;

        public SubscribeController(EduHomeDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe(SubscribeVM subscribeVM)
        {
            Subscribe subscribe = new Subscribe()
            {
                Email = subscribeVM.Email,
            };

            await _context.Subscribes.AddAsync(subscribe);
            await _context.SaveChangesAsync();
            return View();
           
        }
    }
}
