using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ShopCategory> ShopCategories { get; set; }
    }
}
