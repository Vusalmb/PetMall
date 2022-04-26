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
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Employees.Count() / 2);

            List<Employee> employees = _context.Employees.Skip((page-1)*2).Take(2).ToList();

            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Employees.Add(employee);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            Employee employee = _context.Employees.FirstOrDefault(e => e.Id == id);

            if(employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee)
        {
            Employee existEmployee = _context.Employees.FirstOrDefault(e => e.Id == employee.Id);

            if(existEmployee == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(existEmployee);
            }

            existEmployee.Name = employee.Name;
            existEmployee.Specialty = employee.Specialty;
            existEmployee.Desc = employee.Desc;
            existEmployee.Phone = employee.Phone;
            existEmployee.Email = employee.Email;
            existEmployee.FacebookIcon = employee.FacebookIcon;
            existEmployee.FacebookUrl = employee.FacebookUrl;
            existEmployee.TweeterIcon = employee.TweeterIcon;
            existEmployee.TweeterUrl = employee.TweeterUrl;
            existEmployee.PinterestIcon = employee.PinterestIcon;
            existEmployee.PinterestUrl = employee.PinterestUrl;
            existEmployee.InstagramIcon = employee.InstagramIcon;
            existEmployee.InstagramUrl = employee.InstagramUrl;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Employee employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            Employee existEmployee = _context.Employees.FirstOrDefault(e => e.Id == employee.Id);

            if(existEmployee == null)
            {
                return NotFound();
            }

            if (employee == null)
            {
                return Json(new { status = 404 });
            }

            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }
    }
}
