using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetMall.DAL;
using PetMall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public ContactController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index(int page=1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Contacts.Count() / 5);

            List<Contact> contact = _context.Contacts.Skip((page-1)*5).Take(5).ToList();
            return View(contact);
        }

        public IActionResult Delete(int id)
        {
            Contact contact = _context.Contacts.SingleOrDefault(c => c.Id == id);
            if (contact == null) return Json(new { status = 404 });

            _context.Contacts.Remove(contact);
            _context.SaveChanges();

            return Json(new { status = 200 });

        }
    }
}
