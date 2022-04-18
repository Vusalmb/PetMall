using Microsoft.AspNetCore.Mvc;
using PetMall.DAL;
using PetMall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.Controllers
{
    public class FaqsController : Controller
    {
        private readonly AppDbContext _context;
        public FaqsController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<AskedQuestion> question = _context.AskedQuestions.ToList();
            return View(question);
        }
    }
}
