using Microsoft.EntityFrameworkCore;
using Wardrobe.Data;
using Wardrobe.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// DbContext için PostgreSQL baðlantýsý
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity servisi ve yapýlandýrmasý
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// MVC ve Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Middleware pipeline
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Route tanýmlamasý
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Outfits}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
