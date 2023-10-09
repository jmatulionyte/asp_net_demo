using Microsoft.EntityFrameworkCore;
using Twest2.Data;
using Microsoft.AspNetCore.Identity;
using Twest2.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//context for database
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString
   ));

//context for user identity
//builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(connectionString));

//using diff db context?
//.AddEntityFrameworkStores<DataContext>(); -> changed to
//-> .AddEntityFrameworkStores<ApplicationDbContext>(); -> only then works
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

//support for login razor pages?
builder.Services.AddRazorPages();

//to view changes without full restart of page
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); //for identity ui pages
app.Run();

