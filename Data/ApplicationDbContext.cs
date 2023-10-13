using System;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Twest2.Areas.Identity.Data;
using Twest2.Models;

namespace Twest2.Data;
//IdentityDbContext is a base class of ApplicationDbContext, which provides connection to database
public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{

	}

	//creates table in DB
	public DbSet<Category> Categories { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<NewTournament> NewTournaments { get; set; }

}

