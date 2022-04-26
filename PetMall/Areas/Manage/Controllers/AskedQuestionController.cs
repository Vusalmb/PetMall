using Microsoft.AspNetCore.Mvc;
using PetMall.DAL;
using PetMall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AskedQuestionController : Controller
    {
        private readonly AppDbContext _context;
        public AskedQuestionController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.AskedQuestions.Count() / 2);

            List<AskedQuestion> model = _context.AskedQuestions.Skip((page-1)*2).Take(2).ToList();

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AskedQuestion question)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.AskedQuestions.Add(question);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            AskedQuestion question = _context.AskedQuestions.FirstOrDefault(aq => aq.Id == id);

            if(question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AskedQuestion question)
        {
            AskedQuestion existQuestion = _context.AskedQuestions.FirstOrDefault(aq => aq.Id == question.Id);

            if(existQuestion == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(existQuestion);
            }

            existQuestion.Title = question.Title;
            existQuestion.Desc = question.Desc;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            AskedQuestion question = _context.AskedQuestions.FirstOrDefault(aq => aq.Id == id);
            AskedQuestion existQuestion = _context.AskedQuestions.FirstOrDefault(aq => aq.Id == question.Id);

            if(existQuestion == null)
            {
                return NotFound();
            }

            if (question == null)
            {
                return Json(new { status = 404 });
            }

            _context.AskedQuestions.Remove(question);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }
    }
}
