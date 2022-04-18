using PetMall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.ViewModels
{
    public class ShopSearchVM
    {
        public IQueryable<Shop> Shops { get; set; }
        public string Text { get; set; }
    }
}
