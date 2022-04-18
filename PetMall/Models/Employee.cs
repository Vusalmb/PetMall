using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Specialty { get; set; }
        [Required]
        [StringLength(maximumLength: 500)]
        public string Desc { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Phone { get; set; }
        [Required]
        [StringLength(maximumLength: 150)]
        public string Email { get; set; }
        [Required]
        public string FacebookIcon { get; set; }
        [StringLength(maximumLength: 150)]
        public string FacebookUrl { get; set; }
        [Required]
        public string TweeterIcon { get; set; }
        [StringLength(maximumLength: 150)]
        public string TweeterUrl { get; set; }
        [Required]
        public string PinterestIcon { get; set; }
        [StringLength(maximumLength: 150)]
        public string PinterestUrl { get; set; }
        [Required]
        public string InstagramIcon { get; set; }
        [StringLength(maximumLength: 150)]
        public string InstagramUrl { get; set; }
    }
}
