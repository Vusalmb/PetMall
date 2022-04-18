using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public double TotalPrice { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public bool? Status { get; set; }
        public string Message { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
