using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SomeFacts.Data;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(
    @"Data Source=(localdb)\MSSQLLocalDB; Database=SomeFactsSite; Persist Security Info=false; MultipleActiveResultSets=True; Trusted_Connection=True;"
    ))
.AddIdentity<IdentityUser, IdentityRole>(config =>
{
    config.Password.RequireDigit = false;
    config.Password.RequireLowercase = false;
    config.Password.RequireUppercase = false;
    config.Password.RequiredLength = 9;
    config.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<AppDbContext>();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/Account/Login";
});

//builder.Services.AddAuthentication("Cookies").AddCookie("Cookies");

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(RoleNames.Administrator, builder =>
    {
        builder.RequireClaim(ClaimTypes.Role, RoleNames.Administrator);
    });
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

using (var scope = app.Services.CreateScope())
{
    DataBaseInitializer.Init(scope.ServiceProvider);
}

app.Run();
