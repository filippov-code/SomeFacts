using Microsoft.EntityFrameworkCore;
using SomeFacts.Domains;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(
    @"Data Source=(localdb)\MSSQLLocalDB; Database=SomeFactsSite; Persist Security Info=false; MultipleActiveResultSets=True; Trusted_Connection=True;"
    ));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
