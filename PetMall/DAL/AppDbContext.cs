using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetMall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMall.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }

        public DbSet<Setting> Settings { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Join> Joins { get; set; }
        public DbSet<Provide> Provides { get; set; }
        public DbSet<DogCare> DogCares { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<AskedQuestion> AskedQuestions { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<SizeCategory> SizeCategories { get; set; }
        public DbSet<SizeFeature> SizeFeatures { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShopTag> ShopTags { get; set; }
        public DbSet<ShopCategory> ShopCategories { get; set; }
        public DbSet<ShopSize> ShopSizes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Wish> Wishs { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
