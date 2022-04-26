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
    public class JoinController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public JoinController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _env = environment;
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Joins.Count() / 2);

            List<Join> model = _context.Joins.Skip((page-1)*2).Take(2).ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Join join)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (join.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Şəkil daxil edin");
                return View();
            }

            if (join.ImageFile.Length / 1024 / 1024 >= 2)
            {
                ModelState.AddModelError("ImageFile", "Şəklin ölçüsü maksimum 2Mb ola bilər");
                return View();
            }

            if (!join.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "Şəkil daxil edin");
                return View();
            }

            string rootPath = @"D:\Code Academy P223\Layihələr\Final Layihə BackEnd\PetMall\PetMall\wwwroot\assets\image\";

            string fileName = Guid.NewGuid().ToString() + join.ImageFile.FileName;
            string fullPath = Path.Combine(rootPath, fileName);
            using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
            {
                join.ImageFile.CopyTo(fileStream);
            }
            join.Image = fileName;
            _context.Joins.Add(join);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            Join join = _context.Joins.FirstOrDefault(j => j.Id == id);

            if(join == null)
            {
                return NotFound();
            }

            return View(join);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Join join)
        {
            Join existJoin = _context.Joins.FirstOrDefault(j => j.Id == join.Id);

            if (existJoin == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(existJoin);
            }

            if (join.ImageFile != null)
            {
                if (!join.ImageFile.IsSizeOkay())
                {
                    ModelState.AddModelError("ImageFile", "Please select image file");
                    return View(existJoin);
                }

                if (!join.ImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("ImageFile", "Image size must be max 2Mb");
                    return View(existJoin);
                }


                if (existJoin.Image != null)
                {
                    Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/image", existJoin.Image);
                }
                existJoin.Image = join.ImageFile.SaveImg(_env.WebRootPath, "assets/image");
            }
            else
            {
                ModelState.AddModelError("ImageFile", "Please insert an image");
                return View(existJoin);
            }

            existJoin.Icon = join.Icon;
            existJoin.IconUrl = join.IconUrl;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Join join = _context.Joins.FirstOrDefault(j => j.Id == id);
            Join existJoin = _context.Joins.FirstOrDefault(j => j.Id == join.Id);

            if(existJoin == null)
            {
                return NotFound();
            }

            if (join == null)
            {
                return Json(new { status = 404 });
            }

            _context.Joins.Remove(join);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }
    }
}
