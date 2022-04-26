using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetMall.DAL;
using PetMall.Models;
using PetMall.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ContactController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            ContactVM model = new ContactVM
            {
                Setting = _context.Settings.FirstOrDefault()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Message(Contact msg)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Contact model = new Contact
            {
                Id = msg.Id,
                Name = msg.Name,
                Email = msg.Email,
                Phone = msg.Phone,
                Message = msg.Message
            };

            _context.Contacts.Add(model);
            _context.SaveChanges();

            return RedirectToAction("index", "home");
        }
    }
}
