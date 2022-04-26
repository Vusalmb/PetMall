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
    public class SizeFeatureController : Controller
    {
        private readonly AppDbContext _context;
        public SizeFeatureController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.SizeFeatures.Count() / 3);

            List<SizeFeature> model = _context.SizeFeatures.Skip((page-1)*3).Take(3).ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SizeFeature feature)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.SizeFeatures.Add(feature);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            SizeFeature feature = _context.SizeFeatures.FirstOrDefault(sf => sf.Id == id);

            if(feature == null)
            {
                return NotFound();
            }

            return View(feature);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SizeFeature feature)
        {
            SizeFeature existFeature = _context.SizeFeatures.FirstOrDefault(sf => sf.Id == feature.Id);

            if(existFeature == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(existFeature);
            }

            existFeature.Name = feature.Name;
            existFeature.Desc = feature.Desc;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            SizeFeature feature = _context.SizeFeatures.FirstOrDefault(sf => sf.Id == id);
            SizeFeature existFeature = _context.SizeFeatures.FirstOrDefault(sf => sf.Id == feature.Id);

            if(existFeature == null)
            {
                return NotFound();
            }

            if (feature == null)
            {
                return Json(new { status = 404 });
            }

            _context.SizeFeatures.Remove(feature);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }
    }
}
