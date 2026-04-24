using Flagging.Taspen.Web.DBContext;
using Flagging.Taspen.Web.Helpers;
using Flagging.Taspen.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppDataContext>(option => option.UseSqlite(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddIdentity<AppUser, IdentityRole>(
    //option =>
    //{
    //    option.Password.RequiredUniqueChars = 0;
    //    option.Password.RequireUppercase = false;
    //    option.Password.RequireLowercase = false;
    //    option.Password.RequiredLength = 8;
    //    option.Password.RequireNonAlphanumeric = false;
    //}
    ).AddEntityFrameworkStores<AppDataContext>().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/Login";
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDataContext>();
    await db.Database.MigrateAsync(); // Creates DB + applies any pending migrations
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var userManager = serviceProvider.GetService<UserManager<AppUser>>();
    var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

    var context = serviceProvider.GetRequiredService<AppDataContext>();
    await SeedHelper.SeedUserAndRoles(userManager, roleManager, context);
    await SeedHelper.SeedProvinsiKota(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
