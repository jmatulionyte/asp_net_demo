using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using pingPongAPI.Models;

namespace pingPongAPI.Data
{
    //IdentityDbContext instead of DB context , for isentity service. uses applicationuser class for handling users
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>  options) : base(options)
        {
        }
        public DbSet<Player> Players { get; set; }
    }
}

