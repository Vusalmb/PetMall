using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.Models
{
    public class ShopSize
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Shop> Shops { get; set; }
    }
}
