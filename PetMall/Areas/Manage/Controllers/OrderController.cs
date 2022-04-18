using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetMall.DAL;
using PetMall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PetMall.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Orders.Count() / 5);

            List<Order> model = _context.Orders.Skip((page-1)*5).Take(5).ToList();

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            Order order = _context.Orders.Include(o=>o.OrderItems).Include(o => o.AppUser).FirstOrDefault(o => o.Id == id);

            if(order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public IActionResult Accept(int id, string message)
        {
            Order order = _context.Orders.FirstOrDefault(o => o.Id == id);

            if(order == null)
            {
                return Json(new { status = 400 });
            }

            order.Status = true;
            order.Message = message;
            _context.SaveChanges();

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("vusal.bagirov.1501@gmail.com", "Vusal");
            mail.To.Add(new MailAddress("vusalmb@code.edu.az"));
            mail.Subject = "Order";
            mail.Body = $"{message}";
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("vusal.bagirov.1501@gmail.com", "samurai warriors 2");
            smtp.Send(mail);

            return Json(new { status = 200 });
        }
    }
}
