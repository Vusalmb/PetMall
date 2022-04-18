using PetMall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.ViewModels
{
    public class AboutVM
    {
        public Setting Setting { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
