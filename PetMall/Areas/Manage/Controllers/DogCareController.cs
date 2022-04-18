using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PetMall.DAL;
using PetMall.Extension;
using PetMall.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class DogCareController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public DogCareController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _env = environment;
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.DogCares.Count() / 2);

            List<DogCare> model = _context.DogCares.Skip((page-1)*2).Take(2).ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DogCare care)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (care.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Şəkil daxil edin");
                return View();
            }

            if (care.ImageFile.Length / 1024 / 1024 >= 2)
            {
                ModelState.AddModelError("ImageFile", "Şəklin ölçüsü maksimum 2Mb ola bilər");
                return View();
            }

            if (!care.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "Şəkil daxil edin");
                return View();
            }

            string rootPath = @"D:\Code Academy P223\Layihələr\Final Layihə BackEnd\PetMall\PetMall\wwwroot\assets\image\";

            string fileName = Guid.NewGuid().ToString() + care.ImageFile.FileName;
            string fullPath = Path.Combine(rootPath, fileName);
            using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
            {
                care.ImageFile.CopyTo(fileStream);
            }

            care.Image = fileName;
            _context.DogCares.Add(care);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            DogCare care = _context.DogCares.FirstOrDefault(dc => dc.Id == id);

            if(care == null)
            {
                return NotFound();
            }

            return View(care);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DogCare care)
        {
            DogCare existCare = _context.DogCares.FirstOrDefault(dc => dc.Id == care.Id);

            if(existCare == null)
            {
                NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(existCare);
            }

            if (care.ImageFile != null)
            {
                if (!care.ImageFile.IsSizeOkay())
                {
                    ModelState.AddModelError("ImageFile", "Please select image file");
                    return View(existCare);
                }

                if (!care.ImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("ImageFile", "Image size must be max 2Mb");
                    return View(existCare);
                }


                if (existCare.Image != null)
                {
                    Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/image", existCare.Image);
                }
                existCare.Image = care.ImageFile.SaveImg(_env.WebRootPath, "assets/image");
            }
            else
            {
                ModelState.AddModelError("ImageFile", "Please insert an image");
                return View(existCare);
            }

            existCare.Title = care.Title;
            existCare.Desc = care.Desc;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            DogCare care = _context.DogCares.FirstOrDefault(dc => dc.Id == id);
            DogCare existCare = _context.DogCares.FirstOrDefault(dc => dc.Id == care.Id);

            if(existCare == null)
            {
                return NotFound();
            }

            if (care == null)
            {
                return Json(new { status = 404 });
            }

            _context.DogCares.Remove(care);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }
    }
}
