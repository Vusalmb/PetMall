using PetMall.Extension;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PetMall.DAL;
using PetMall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetMall.Helpers;

namespace PetMall.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SettingController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _env = environment;
        }
        public IActionResult Index()
        {
            Setting model = _context.Settings.FirstOrDefault();
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            Setting setting = _context.Settings.FirstOrDefault(s=>s.Id == id);

            if(setting == null)
            {
                return NotFound();
            }

            return View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Setting setting)
        {
            Setting existSetting = _context.Settings.FirstOrDefault(s => s.Id == setting.Id);

            if (!ModelState.IsValid)
            {
                return View(existSetting);
            }

            if (existSetting == null)
            {
                return NotFound();
            }

            if (setting.LogoFile != null)
            {
                if (!setting.LogoFile.IsSizeOkay())
                {
                    ModelState.AddModelError("LogoFile", "Please select image file");
                    return View(existSetting);
                }

                if (!setting.LogoFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("LogoFile", "Image size must be max 2Mb");
                    return View(existSetting);
                }


                if (existSetting.Logo != null)
                {
                    Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/image", existSetting.Logo);
                }
                existSetting.Logo = setting.LogoFile.SaveImg(_env.WebRootPath, "assets/image");
            }
            else
            {
                ModelState.AddModelError("LogoFile", "Please insert an image");
                return View(existSetting);
            }

            if (setting.LogoImageFile != null)
            {
                if (!setting.LogoImageFile.IsSizeOkay())
                {
                    ModelState.AddModelError("LogoImageFile", "Please select image file");
                    return View(existSetting);
                }

                if (!setting.LogoImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("LogoImageFile", "Image size must be max 2Mb");
                    return View(existSetting);
                }


                if (existSetting.LogoImage != null)
                {
                    Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/image", existSetting.LogoImage);
                }
                existSetting.LogoImage = setting.LogoImageFile.SaveImg(_env.WebRootPath, "assets/image");
            }
            else
            {
                ModelState.AddModelError("LogoImageFile", "Please insert an image");
                return View(existSetting);
            }

            existSetting.LogoDesc = setting.LogoDesc;
            existSetting.SearchIcon = setting.SearchIcon;
            existSetting.BasketIcon = setting.BasketIcon;
            existSetting.SettingIcon = setting.SettingIcon;
            existSetting.ScrollTopIcon = setting.ScrollTopIcon;
            existSetting.FacebookIcon = setting.FacebookIcon;
            existSetting.FacebookUrl = setting.FacebookUrl;
            existSetting.PinterestIcon = setting.PinterestIcon;
            existSetting.PinterestUrl = setting.PinterestUrl;
            existSetting.InstagramIcon = setting.InstagramIcon;
            existSetting.InstagramUrl = setting.InstagramUrl;
            existSetting.TweeterIcon = setting.TweeterIcon;
            existSetting.TweeterUrl = setting.TweeterUrl;
            existSetting.Address = setting.Address;
            existSetting.Phone = setting.Phone;
            existSetting.Email = setting.Email;
            existSetting.OpenTime = setting.OpenTime;
            existSetting.OurCompany = setting.OurCompany;
            existSetting.OurTeam = setting.OurTeam;
            existSetting.CompanyQuote = setting.CompanyQuote;
            existSetting.Conditions = setting.Conditions;
            existSetting.Cookies = setting.Cookies;
            existSetting.PrivacyPolicy = setting.PrivacyPolicy;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
