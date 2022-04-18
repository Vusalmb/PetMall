using Microsoft.AspNetCore.Mvc;
using PetMall.DAL;
using PetMall.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int id)
        {
            HomeVM model = new HomeVM
            {
                Setting = _context.Settings.FirstOrDefault(),
                Sliders = _context.Sliders.ToList(),
                Joins = _context.Joins.ToList(),
                Provides = _context.Provides.ToList(),
                DogCares = _context.DogCares.ToList()
            };

            ViewBag.BlogOrder = _context.Blogs.OrderByDescending(b => b.Id == id).Take(12).ToList();
            ViewBag.ShopOrder = _context.Shops.OrderByDescending(s => s.Id == id).Take(6).ToList();

            return View(model);
        }
    }
}
