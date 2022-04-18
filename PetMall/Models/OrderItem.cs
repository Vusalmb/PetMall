using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Shop Shop { get; set; }
        public int? ShopId { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
    }
}
