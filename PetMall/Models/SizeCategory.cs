using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.Models
{
    public class SizeCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public Size Size { get; set; }
    }
}
