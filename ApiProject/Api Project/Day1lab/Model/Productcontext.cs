using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Day1lab.Model
{
    public class Productcontext : IdentityDbContext<ApplicationUser>
    {
        public Productcontext()
        {

        }
        public Productcontext(DbContextOptions options) : base(options)
        {

            //Database.EnsureCreated();

        }
      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=WebAPI;Integrated Security=True");
        }
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<CartItem> CartItems { get; set;}
        public DbSet<ApplicationUser> User { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Blog>()
        //        .ToTable("User", schema: "Productcontext");
        //}
    }
}
