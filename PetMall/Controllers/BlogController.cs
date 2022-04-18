using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetMall.DAL;
using PetMall.Models;
using PetMall.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        public BlogController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page=1)
        {
            List<Blog> blogs = _context.Blogs.Skip((page - 1) * 3).Take(3).ToList();
            ViewBag.BlogOrder = _context.Blogs.OrderByDescending(b => b.Id).Take(5).ToList();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Blogs.Count() / 3);

            return View(blogs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string search = "")
        {
            ViewBag.BlogOrder = _context.Blogs.OrderByDescending(b => b.Id).Take(5).ToList();
            List<Blog> blogs;

            if (search != "" && search != null)
            {
                blogs = _context.Blogs.Where(b => b.Title.Contains(search)).ToList();
            }
            else
            {
                blogs = _context.Blogs.ToList();
            }

            return View(blogs);
        }

        public IActionResult Detail(int id)
        {
            BlogVM model = new BlogVM
            {
                Blog = _context.Blogs.FirstOrDefault(b=>b.Id == id),
                Setting = _context.Settings.FirstOrDefault()
            };

            ViewBag.BlogOrder = _context.Blogs.OrderByDescending(b => b.Id).Take(5).ToList();

            return View(model);
        }

        public IActionResult CategoryBlog(int id)
        {
            List<Blog> blogs = _context.Blogs.Where(c => c.BlogCategoryId == id).ToList();
            ViewBag.CategoryOrder = _context.BlogCategories.Include(bc => bc.Blogs).ToList();
            return View(blogs);
        }
    }
}
