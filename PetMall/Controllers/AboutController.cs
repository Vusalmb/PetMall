using Microsoft.AspNetCore.Mvc;
using PetMall.DAL;
using PetMall.Models;
using PetMall.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        public AboutController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            AboutVM model = new AboutVM
            {
                Setting = _context.Settings.FirstOrDefault(),
                Employees = _context.Employees.ToList()
            };
            return View(model);
        }
    }
}
