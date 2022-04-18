using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.Models
{
    public class Size
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public SizeCategory SizeCategory { get; set; }
        public int SizeCategoryId { get; set; }
    }
}
