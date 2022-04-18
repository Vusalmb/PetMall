using PetMall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.ViewModels
{
    public class HomeVM
    {
        public Setting Setting { get; set; }
        public List<Slider> Sliders { get; set; }
        public List<Join> Joins { get; set; }
        public List<Provide> Provides { get; set; }
        public List<DogCare> DogCares { get; set; }
    }
}
