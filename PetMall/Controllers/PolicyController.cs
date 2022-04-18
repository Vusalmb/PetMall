using Microsoft.AspNetCore.Mvc;
using PetMall.DAL;
using PetMall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.Controllers
{
    public class PolicyController : Controller
    {
        private readonly AppDbContext _context;
        public PolicyController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            Setting policy = _context.Settings.FirstOrDefault();

            return View(policy);
        }
    }
}
