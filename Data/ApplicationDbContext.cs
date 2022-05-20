using System;
using System.Text;
using System.Collections.Generic;
using KosovoTeam.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace KosovoTeam.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor_Movie>().HasKey(am => new
            {
                am.PlayerId,
                am.ProductId
            });

            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Product).WithMany(am => am.Actors_Movies).HasForeignKey(m => m.ProductId);
            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Player).WithMany(am => am.Actors_Movies).HasForeignKey(m => m.PlayerId);


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Actor_Movie> Actors_Movies { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Staff> Staffs { get; set; }

        //Data related tables

        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        //Order related tables

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<KosovoTeam.Models.Category> Category { get; set; }

        public DbSet<Coupon> Coupons { get; set; }
    }
}
