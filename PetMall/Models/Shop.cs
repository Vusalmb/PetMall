using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.Models
{
    public class Shop
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 150)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [Required]
        [StringLength(maximumLength: 150)]
        public string Title { get; set; }
        [Required]
        [StringLength(maximumLength: 500)]
        public string Desc { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Weight { get; set; }
        [Required]
        public string SKUCode { get; set; }
        public List<ShopTag> ShopTags { get; set; }
        public List<ShopCategory> ShopCategories { get; set; }
        [NotMapped]
        public List<int> TagIds { get; set; }
        [NotMapped]
        public List<int> CategoryIds { get; set; }
        public ShopSize ShopSize { get; set; }
        public int ShopSizeId { get; set; }
        public List<Comment> Comments { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        [NotMapped]
        public Comment Comment { get; set; }
        public List<Wish> Wishs { get; set; }
    }
}
