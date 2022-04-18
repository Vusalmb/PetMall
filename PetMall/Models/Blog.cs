using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.Models
{
    public class Blog
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 150)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [StringLength(maximumLength: 150)]
        public string Title { get; set; }
        public string Desc { get; set; }
        public DateTime Date { get; set; }
        public BlogCategory BlogCategory { get; set; }
        public int BlogCategoryId { get; set; }
    }
}
