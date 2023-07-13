﻿using ecommerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Data
{



    public class EcommerceContext : IdentityDbContext<ApplicationUser>
    {
        public EcommerceContext(DbContextOptions<EcommerceContext> options)
         : base(options)

        { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
       
        }
    }
