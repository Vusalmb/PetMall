using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PetMall.DAL;
using PetMall.Models;
using PetMall.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public ShopController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Shops.Count() / 12);
            List<Shop> shops = _context.Shops.Skip((page-1)*12).Take(12).ToList();
            return View(shops);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string search = "")
        {
            List<Shop> shops;

            if(search != "" && search != null)
            {
                shops = _context.Shops.Where(s => s.Title.Contains(search)).ToList();
            }
            else
            {
                shops = _context.Shops.ToList();
            }

            return View(shops);
        }

        public IActionResult Detail(int id)
        {
            ViewBag.ShopOrder = _context.Shops.OrderByDescending(s => s.Id == id).Take(10).ToList();
            Shop shop = _context.Shops.Include(s=>s.ShopTags).ThenInclude(st=>st.Tag).Include(s => s.ShopCategories).ThenInclude(sc => sc.Category).Include(s=>s.ShopSize).Include(s=>s.Comments).ThenInclude(c=>c.AppUser).FirstOrDefault(s => s.Id == id);
            return View(shop);
        }

        public async Task<IActionResult> Wish(int id)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            

            Shop shop = _context.Shops.FirstOrDefault(s => s.Id == id);
            return View(shop);
        }

        [Authorize]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddComment(Shop shop)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            //if (!ModelState.IsValid)
            //{
            //    return RedirectToAction("Detail", "Shop", new { id = shop.Id });
            //}

            Comment cmnt = new Comment
            {
                Title = shop.Comment.Title,
                Text = shop.Comment.Text,
                ShopId = shop.Comment.ShopId,
                AppUserId = user.Id,
                CreateDate = DateTime.Now,
            };
            _context.Comments.Add(cmnt);
            _context.SaveChanges();

            return RedirectToAction("detail", "shop", new { id = shop.Comment.ShopId });
        }

        [Authorize]
        public async Task<IActionResult> DeleteComment(int id)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Shop");
            }

            if (!_context.Comments.Any(c => c.Id == id && c.IsAccess == true && c.AppUserId == user.Id))
            {
                return NotFound();
            }

            Comment comment = _context.Comments.FirstOrDefault(c => c.Id == id && c.AppUserId == user.Id);
            _context.Comments.Remove(comment);
            _context.SaveChanges();

            return RedirectToAction("detail", "shop", new { id = comment.ShopId });
        }

        public async Task<IActionResult> AddBasket(int id)
        {
            Shop shop = _context.Shops.FirstOrDefault(s => s.Id == id);

            if(User.Identity.IsAuthenticated && !User.IsInRole("SuperAdmin"))
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                BasketItem basketItem = _context.BasketItems.FirstOrDefault(bi => bi.ShopId == shop.Id && bi.AppUserId == user.Id);

                if(basketItem == null)
                {
                    basketItem = new BasketItem
                    {
                        AppUserId = user.Id,
                        ShopId = shop.Id,
                        Count = 1
                    };
                    _context.BasketItems.Add(basketItem);
                }
                else
                {
                    basketItem.Count++;
                }
                _context.SaveChanges();
            }
            else
            {
                string basket = HttpContext.Request.Cookies["Basket"];

                if (basket == null)
                {
                    List<BasketCookieItemVM> basketCookieItems = new List<BasketCookieItemVM>();

                    basketCookieItems.Add(new BasketCookieItemVM
                    {
                        Id = shop.Id,
                        Count = 1
                    });

                    string basketStr = JsonConvert.SerializeObject(basketCookieItems);
                    HttpContext.Response.Cookies.Append("Basket", basketStr);
                }
                else
                {
                    List<BasketCookieItemVM> basketCookieItems = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(basket);
                    BasketCookieItemVM cookieItem = basketCookieItems.FirstOrDefault(c => c.Id == shop.Id);

                    if (cookieItem == null)
                    {
                        cookieItem = new BasketCookieItemVM
                        {
                            Id = shop.Id,
                            Count = 1
                        };
                        basketCookieItems.Add(cookieItem);
                    }
                    else
                    {
                        cookieItem.Count++;
                    }

                    string basketStr = JsonConvert.SerializeObject(basketCookieItems);
                    HttpContext.Response.Cookies.Append("Basket", basketStr);
                }
            }

            return RedirectToAction("index", "shop");
        }

        public async Task<IActionResult> DeleteBasketItem(int id)
        {
            Shop shop = _context.Shops.FirstOrDefault(s=>s.Id==id);
            if (User.Identity.IsAuthenticated && !User.IsInRole("SuperAdmin"))
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                BasketItem basketItem = _context.BasketItems.FirstOrDefault(bi => bi.ShopId == shop.Id && bi.AppUserId == user.Id);

                _context.BasketItems.Remove(basketItem);
                _context.SaveChanges();
            }
            else
            {
                string basket = HttpContext.Request.Cookies["Basket"];

                    List<BasketCookieItemVM> basketCookieItems = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(basket);
                    BasketCookieItemVM cookieItem = basketCookieItems.FirstOrDefault(c => c.Id == shop.Id);

                        basketCookieItems.Remove(cookieItem);

                    string basketStr = JsonConvert.SerializeObject(basketCookieItems);
                    HttpContext.Response.Cookies.Append("Basket", basketStr);
                
            }
            return RedirectToAction("index", "home");
        }

        public IActionResult ShowBasket()
        {
            string basketStr = HttpContext.Request.Cookies["Basket"];
            if (!string.IsNullOrEmpty(basketStr))
            {
                var basket = JsonConvert.DeserializeObject(basketStr);
                return Content(basket.ToString());
            }

            return Content("Basket is empty");
        }
    }
}
