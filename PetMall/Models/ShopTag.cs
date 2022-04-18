using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.Models
{
    public class ShopTag
    {
        public int Id { get; set; }
        public Shop Shop { get; set; }
        public Tag Tag { get; set; }
        public int ShopId { get; set; }
        public int TagId { get; set; }
    }
}
