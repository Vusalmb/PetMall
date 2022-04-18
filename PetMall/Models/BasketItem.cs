using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public Shop Shop { get; set; }
        public int ShopId { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
        public int Count { get; set; }
    }
}
