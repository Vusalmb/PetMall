using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.Models
{
    public class AskedQuestion
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
    }
}
