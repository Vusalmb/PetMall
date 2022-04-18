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
    public class SizeController : Controller
    {
        private readonly AppDbContext _context;
        public SizeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            SizeVM model = new SizeVM
            {
                Sizes = _context.Sizes.Include(s => s.SizeCategory).OrderBy(sc => sc.SizeCategory.Order).ToList(),
                SizeFeatures = _context.SizeFeatures.ToList()
            };

            return View(model);
        }
    }
}
