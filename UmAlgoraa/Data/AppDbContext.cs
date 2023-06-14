using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UmAlgoraa.Models;
using UmAlgoraa.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UmAlgoraa.Controllers.Data
{
    // generic idenitiy , allowing me to use my own cutme appliction user or class 
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        // GET: /<controller>/
       public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {


        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Ads> Ads { get; set; }

        //public DbSet<Draft> Drafts { get; set; }

    }
}

