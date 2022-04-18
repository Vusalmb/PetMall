using PetMall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.ViewModels
{
    public class SizeVM
    {
        public List<Size> Sizes { get; set; }
        public List<SizeFeature> SizeFeatures { get; set; }
    }
}
