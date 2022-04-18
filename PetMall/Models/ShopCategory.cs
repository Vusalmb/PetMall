using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.Models
{
    public class ShopCategory
    {
        public int Id { get; set; }
        public Shop Shop { get; set; }
        public Category Category { get; set; }
        public int ShopId { get; set; }
        public int CategoryId { get; set; }
    }
}
