using PetMall.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.ViewModels
{
    public class OrderVM
    {
        [Required]
        [StringLength(maximumLength: 80)]
        public string FullName { get; set; }
        [Required]
        [StringLength(maximumLength: 80)]
        public string UserName { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string Address { get; set; }
        [Required]
        [StringLength(maximumLength: 80)]
        public string Country { get; set; }
        [Required]
        [StringLength(maximumLength: 80)]
        public string State { get; set; }
        public List<BasketItem> BasketItems { get; set; }
    }
}
