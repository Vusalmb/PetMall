using Microsoft.AspNetCore.Http;
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

namespace PetMall.Services
{
    public class LayoutServices
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public LayoutServices(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContext = httpContextAccessor;
        }

        public Setting GetSettingDatas()
        {
            Setting data = _context.Settings.FirstOrDefault();
            return data;
        }

        public BasketVM ShowBasket()
        {
            string basket = _httpContext.HttpContext.Request.Cookies["Basket"];
            BasketVM basketVM = new BasketVM();
            BasketVM basketData = new BasketVM
            {
                BasketItems = new List<BasketItemVM>(),
                TotalPrice = 0,
                Count = 0

            };

            if (_httpContext.HttpContext.User.Identity.IsAuthenticated)
            {
                List<BasketItem> basketItems = _context.BasketItems.Include(bi => bi.AppUser).Where(u => u.AppUser.UserName == _httpContext.HttpContext.User.Identity.Name).ToList();

                foreach (BasketItem item in basketItems)
                {
                    Shop shop = _context.Shops.FirstOrDefault(s => s.Id == item.ShopId);

                    if(shop != null)
                    {
                        BasketItemVM basketItemVM = new BasketItemVM
                        {
                            Shop = shop,
                            Count = item.Count
                        };
                        basketItemVM.Price = shop.Price;
                        basketData.BasketItems.Add(basketItemVM);
                        basketData.Count++;
                        basketData.TotalPrice += basketItemVM.Shop.Price * basketItemVM.Count;
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(basket))
                {
                    List<BasketCookieItemVM> basketCookieItems = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(basket);

                    foreach (BasketCookieItemVM item in basketCookieItems)
                    {
                        Shop shop = _context.Shops.FirstOrDefault(s => s.Id == item.Id);
                        if (shop != null)
                        {
                            BasketItemVM basketItem = new BasketItemVM
                            {
                                Shop = _context.Shops.FirstOrDefault(s => s.Id == item.Id),
                                Count = item.Count
                            };

                            basketData.BasketItems.Add(basketItem);
                            basketData.Count++;
                            basketData.TotalPrice += basketItem.Shop.Price * basketItem.Count;
                        }
                    }
                }
            }
            
            return basketData;
        }
    }
}
