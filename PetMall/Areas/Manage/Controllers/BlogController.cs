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
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public BlogController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _env = environment;
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Blogs.Count() / 3);

            List<Blog> model = _context.Blogs.Skip((page - 1) * 3).Take(3).ToList();

            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.BlogCategories = _context.BlogCategories.ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Blog blogs)
        {
            ViewBag.BlogCategories = _context.BlogCategories.ToList();

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (blogs.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Şəkil daxil edin");
                return View();
            }

            if (blogs.ImageFile.Length / 1024 / 1024 >= 2)
            {
                ModelState.AddModelError("ImageFile", "Şəklin ölçüsü maksimum 2Mb ola bilər");
                return View();
            }

            if (!blogs.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "Şəkil daxil edin");
                return View();
            }

            if (!blogs.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "Şəkil daxil edin");
                return View();
            }

            string rootPath = @"D:\Code Academy P223\Layihələr\Final Layihə BackEnd\PetMall\PetMall\wwwroot\assets\image\";
            string fileName = Guid.NewGuid().ToString() + blogs.ImageFile.FileName;
            string fullPath = Path.Combine(rootPath, fileName);
            using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
            {
                blogs.ImageFile.CopyTo(fileStream);
            }
            blogs.Image = fileName;
            _context.Blogs.Add(blogs);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            ViewBag.BlogCategories = _context.BlogCategories.ToList();

            Blog blog = _context.Blogs.FirstOrDefault(b => b.Id == id);

            if(blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Blog blog)
        {
            ViewBag.BlogCategories = _context.BlogCategories.ToList();

            Blog existBlog = _context.Blogs.FirstOrDefault(b => b.Id == blog.Id);

            if (!ModelState.IsValid)
            {
                return View(existBlog);
            }

            if(existBlog == null)
            {
                return NotFound();
            }

            if (blog.ImageFile != null)
            {
                if (!blog.ImageFile.IsSizeOkay())
                {
                    ModelState.AddModelError("ImageFile", "Please select image file");
                    return View(existBlog);
                }

                if (!blog.ImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("ImageFile", "Image size must be max 2Mb");
                    return View(existBlog);
                }


                if (existBlog.Image != null)
                {
                    Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/image", existBlog.Image);
                }
                existBlog.Image = blog.ImageFile.SaveImg(_env.WebRootPath, "assets/image");
            }
            else
            {
                ModelState.AddModelError("ImageFile", "Please insert an image");
                return View(existBlog);
            }

            existBlog.Title = blog.Title;
            existBlog.Desc = blog.Desc;
            existBlog.Date = blog.Date;
            existBlog.BlogCategoryId = blog.BlogCategoryId;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Blog blog = _context.Blogs.FirstOrDefault(b => b.Id == id);
            Blog existBlog = _context.Blogs.FirstOrDefault(b => b.Id == blog.Id);

            if(existBlog == null)
            {
                return NotFound();
            }

            if(blog == null)
            {
                return Json(new { status = 404 });
            }

            _context.Blogs.Remove(blog);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }
    }
}
