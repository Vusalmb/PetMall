using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetMall.DAL;
using PetMall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SizeController : Controller
    {
        private readonly AppDbContext _context;
        public SizeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Size> model = _context.Sizes.Include(s=>s.SizeCategory).ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.SizeCategories = _context.SizeCategories.ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Size size)
        {
            ViewBag.SizeCategories = _context.SizeCategories.ToList();

            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Sizes.Add(size);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            ViewBag.SizeCategories = _context.SizeCategories.ToList();
            Size size = _context.Sizes.FirstOrDefault(s => s.Id == id);

            if(size == null)
            {
                return NotFound();
            }

            return View(size);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Size size)
        {
            ViewBag.SizeCategories = _context.SizeCategories.ToList();
            Size existSize = _context.Sizes.FirstOrDefault(s => s.Id == size.Id);
            
            if(existSize == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(existSize);
            }

            existSize.Desc = size.Desc;
            existSize.SizeCategory.Name = size.SizeCategory.Name;
            existSize.SizeCategory.Order = size.SizeCategory.Order;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Size size = _context.Sizes.FirstOrDefault(s => s.Id == id);
            Size existSize = _context.Sizes.FirstOrDefault(s => s.Id == size.Id);

            if(existSize == null)
            {
                return NotFound();
            }

            if (size == null)
            {
                return Json(new { status = 404 });
            }

            _context.Sizes.Remove(size);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }
    }
}
