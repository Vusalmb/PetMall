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
    public class ProvideController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ProvideController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _env = environment;
        }
        public IActionResult Index()
        {
            List<Provide> model = _context.Provides.ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Provide provide)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (provide.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Şəkil daxil edin");
                return View();
            }

            if (provide.ImageFile.Length / 1024 / 1024 >= 2)
            {
                ModelState.AddModelError("ImageFile", "Şəklin ölçüsü maksimum 2Mb ola bilər");
                return View();
            }

            if (!provide.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "Şəkil daxil edin");
                return View();
            }

            string rootPath = @"D:\Code Academy P223\Layihələr\Final Layihə BackEnd\PetMall\PetMall\wwwroot\assets\image\";

            string fileName = Guid.NewGuid().ToString() + provide.ImageFile.FileName;
            string fullPath = Path.Combine(rootPath, fileName);
            using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
            {
                provide.ImageFile.CopyTo(fileStream);
            }
            provide.Image = fileName;
            _context.Provides.Add(provide);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            Provide provide = _context.Provides.FirstOrDefault(p => p.Id == id);

            if(provide == null)
            {
                return NotFound();
            }

            return View(provide);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Provide provide)
        {
            Provide existProvide = _context.Provides.FirstOrDefault(p => p.Id == provide.Id);

            if (existProvide == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(existProvide);
            }

            if (provide.ImageFile != null)
            {
                if (!provide.ImageFile.IsSizeOkay())
                {
                    ModelState.AddModelError("ImageFile", "Please select image file");
                    return View(existProvide);
                }

                if (!provide.ImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("ImageFile", "Image size must be max 2Mb");
                    return View(existProvide);
                }


                if (existProvide.Image != null)
                {
                    Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/image", existProvide.Image);
                }
                existProvide.Image = provide.ImageFile.SaveImg(_env.WebRootPath, "assets/image");
            }
            else
            {
                ModelState.AddModelError("ImageFile", "Please insert an image");
                return View(existProvide);
            }

            existProvide.Title = provide.Title;
            existProvide.Desc = provide.Desc;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Provide provide = _context.Provides.FirstOrDefault(p => p.Id == id);
            Provide existProvide = _context.Provides.FirstOrDefault(p => p.Id == provide.Id);

            if(existProvide == null)
            {
                return NotFound();
            }

            if (provide == null)
            {
                return Json(new { status = 404 });
            }

            _context.Provides.Remove(provide);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }
    }
}
