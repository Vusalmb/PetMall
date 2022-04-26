using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ShopController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _env = environment;
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Shops.Count() / 5);

            List<Shop> model = _context.Shops.Include(s=>s.Comments).Skip((page - 1) * 5).Take(5).ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.ShopSizes = _context.ShopSizes.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            ViewBag.Categories = _context.Categories.ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Shop shops)
        {
            ViewBag.ShopSizes = _context.ShopSizes.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            ViewBag.Categories = _context.Categories.ToList();

            if (!ModelState.IsValid)
            {
                return View();
            }

            shops.ShopTags = new List<ShopTag>();
            shops.ShopCategories = new List<ShopCategory>();

            foreach (int id in shops.TagIds)
            {
                ShopTag sTag = new ShopTag
                {
                    Shop = shops,
                    TagId = id
                };
                shops.ShopTags.Add(sTag);
            }

            foreach (int id in shops.CategoryIds)
            {
                ShopCategory sCategory = new ShopCategory
                {
                    Shop = shops,
                    CategoryId = id
                };
                shops.ShopCategories.Add(sCategory);
            }

            if (shops.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Şəkil daxil edin");
                return View();
            }

            if (shops.ImageFile.Length / 1024 / 1024 >= 2)
            {
                ModelState.AddModelError("ImageFile", "Şəklin ölçüsü maksimum 2Mb ola bilər");
                return View();
            }

            if (!shops.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "Şəkil daxil edin");
                return View();
            }

            if (!shops.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "Şəkil daxil edin");
                return View();
            }

            string rootPath = @"D:\Code Academy P223\Layihələr\Final Layihə BackEnd\PetMall\PetMall\wwwroot\assets\image\";
            string fileName = Guid.NewGuid().ToString() + shops.ImageFile.FileName;
            string fullPath = Path.Combine(rootPath, fileName);
            using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
            {
                shops.ImageFile.CopyTo(fileStream);
            }
            shops.Image = fileName;
            _context.Shops.Add(shops);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            ViewBag.ShopSizes = _context.ShopSizes.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            ViewBag.Categories = _context.Categories.ToList();

            Shop shop = _context.Shops.Include(s=>s.ShopTags).Include(s => s.ShopCategories).FirstOrDefault(s => s.Id == id);

            if(shop == null)
            {
                return NotFound();
            }

            return View(shop);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Shop shops)
        {
            ViewBag.ShopSizes = _context.ShopSizes.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            ViewBag.Categories = _context.Categories.ToList();

            Shop existShop = _context.Shops.Include(s => s.ShopTags).Include(s => s.ShopCategories).FirstOrDefault(s => s.Id == shops.Id);

            if (existShop == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(existShop);
            }

            if (shops.ImageFile != null)
            {
                if (!shops.ImageFile.IsSizeOkay())
                {
                    ModelState.AddModelError("ImageFile", "Please select image file");
                    return View(existShop);
                }

                if (!shops.ImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("ImageFile", "Image size must be max 2Mb");
                    return View(existShop);
                }


                if (existShop.Image != null)
                {
                    Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/image", existShop.Image);
                }
                existShop.Image = shops.ImageFile.SaveImg(_env.WebRootPath, "assets/image");
            }
            else
            {
                ModelState.AddModelError("ImageFile", "Please insert an image");
                return View(existShop);
            }

            existShop.Title = shops.Title;
            existShop.Desc = shops.Desc;
            existShop.Price = shops.Price;
            existShop.Weight = shops.Weight;
            existShop.SKUCode = shops.SKUCode;

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Shop shop = _context.Shops.FirstOrDefault(s => s.Id == id);
            Shop existShop = _context.Shops.FirstOrDefault(s => s.Id == shop.Id);

            if (existShop == null)
            {
                return NotFound();
            }

            if(shop == null)
            {
                return Json(new { status = 404 });
            }

            _context.Shops.Remove(shop);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }

        public IActionResult Comment(int ShopId)
        {
            if(!_context.Comments.Any(c=>c.ShopId == ShopId))
            {
                return RedirectToAction("index", "shop");
            }

            List<Comment> comments = _context.Comments.Include(c=>c.AppUser).Where(c => c.ShopId == ShopId).ToList();

            return View(comments);
        }

        public IActionResult CommentAccept(int id)
        {
            if (!_context.Comments.Any(c => c.Id == id))
            {
                return RedirectToAction("index", "shop");
            }

            Comment comment = _context.Comments.SingleOrDefault(c => c.Id == id);
            comment.IsAccess = true;
            _context.SaveChanges();

            return RedirectToAction("comment", "shop", new { ShopId = comment.ShopId });
        }

        public IActionResult CommentReject(int id)
        {
            if (!_context.Comments.Any(c => c.Id == id))
            {
                return RedirectToAction("index", "shop");
            }

            Comment comment = _context.Comments.SingleOrDefault(c => c.Id == id);
            comment.IsAccess = false;
            _context.SaveChanges();

            return RedirectToAction("comment", "shop", new { ShopId = comment.ShopId });
        }
    }
}
