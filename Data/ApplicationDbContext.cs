using System;
using System.Net.NetworkInformation;
using Microsoft.EntityFrameworkCore;
using Twest2.Areas.Identity.Data;
using Twest2.Models;

namespace Twest2.Data;

public class ApplicationDbContext :DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{

	}

	public DbSet<Category> Categories { get; set; }
    public DbSet<ApplicationUser> Users { get; set; }

}

