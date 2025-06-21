using Microsoft.EntityFrameworkCore;
using Wardrobe.Data;
using Wardrobe.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

<<<<<<< HEAD
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
=======
// Render veya benzeri platformlarda PORT ortam deðiþkenini kullan
var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders()
.AddDefaultUI();

>>>>>>> 1b9bee640ac8e0c8aff756158b1d76e51c38e248
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

<<<<<<< HEAD
// Middleware pipeline
app.UseStaticFiles();
=======
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

>>>>>>> 1b9bee640ac8e0c8aff756158b1d76e51c38e248
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

<<<<<<< HEAD
// Route tanýmlamasý
=======
>>>>>>> 1b9bee640ac8e0c8aff756158b1d76e51c38e248
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Outfits}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
