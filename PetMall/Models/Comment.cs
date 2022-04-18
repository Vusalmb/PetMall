using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string Title { get; set; }
        [Required]
        [StringLength(maximumLength: 500)]
        public string Text { get; set; }
        public bool IsAccess { get; set; }
        public DateTime CreateDate { get; set; }
        public Shop Shop { get; set; }
        [Required]
        public int ShopId { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
    }
}
