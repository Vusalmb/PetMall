using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    [Authorize(Roles = "SuperAdmin")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public OrderController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Checkout()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            OrderVM model = new OrderVM
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                BasketItems = _context.BasketItems.Include(bi=>bi.Shop).Where(bi => bi.AppUserId == user.Id).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(OrderVM orderVM)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            OrderVM model = new OrderVM
            {
                FullName = orderVM.FullName,
                UserName = orderVM.UserName,
                Email = orderVM.Email,
                BasketItems = _context.BasketItems.Include(bi => bi.Shop).Where(bi => bi.AppUserId == user.Id).ToList()
            };

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            TempData["Successed"] = false;

            if(model.BasketItems.Count == 0)
            {
                return RedirectToAction("index", "shop");
            }

            Order order = new Order
            {
                Country = orderVM.Country,
                State = orderVM.State,
                Address = orderVM.Address,
                TotalPrice = 0,
                Date = DateTime.Now,
                AppUserId = user.Id
            };

            foreach (BasketItem item in model.BasketItems)
            {
                order.TotalPrice += item.Shop.Price * item.Count;
                OrderItem orderItem = new OrderItem
                {
                    Name = item.Shop.Title,
                    Price = item.Shop.Price * item.Count,
                    AppUserId = user.Id,
                    ShopId = item.Shop.Id,
                    Order = order
                };
                _context.OrderItems.Add(orderItem);
            }
            _context.BasketItems.RemoveRange(model.BasketItems);
            _context.Orders.Add(order);
            _context.SaveChanges();
            TempData["Successed"] = true;

            return RedirectToAction("index", "shop");
        }
    }
}
