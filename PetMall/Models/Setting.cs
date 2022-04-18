using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.Models
{
    public class Setting
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 150)]
        public string Logo { get; set; }
        [NotMapped]
        public IFormFile LogoFile { get; set; }
        [StringLength(maximumLength: 150)]
        public string LogoImage { get; set; }
        [NotMapped]
        public IFormFile LogoImageFile { get; set; }
        [StringLength(maximumLength: 500)]
        public string LogoDesc { get; set; }
        [Required]
        public string SearchIcon { get; set; }
        [Required]
        public string BasketIcon { get; set; }
        [Required]
        public string SettingIcon { get; set; }
        [Required]
        public string ScrollTopIcon { get; set; }
        [Required]
        public string FacebookIcon { get; set; }
        [StringLength(maximumLength: 150)]
        public string FacebookUrl { get; set; }
        [Required]
        public string PinterestIcon { get; set; }
        [StringLength(maximumLength: 150)]
        public string PinterestUrl { get; set; }
        [Required]
        public string InstagramIcon { get; set; }
        [StringLength(maximumLength: 150)]
        public string InstagramUrl { get; set; }
        [Required]
        public string TweeterIcon { get; set; }
        [StringLength(maximumLength: 150)]
        public string TweeterUrl { get; set; }
        [Required]
        [StringLength(maximumLength: 150)]
        public string Address { get; set; }
        [Required]
        [StringLength(maximumLength: 150)]
        public string Phone { get; set; }
        [Required]
        [StringLength(maximumLength: 150)]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string OpenTime { get; set; }
        public string OurCompany { get; set; }
        public string OurTeam { get; set; }
        public string CompanyQuote { get; set; }
        public string Conditions { get; set; }
        public string Cookies { get; set; }
        public string PrivacyPolicy { get; set; }
    }
}
