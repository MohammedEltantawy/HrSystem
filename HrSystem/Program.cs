using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HrSystem.WebUI.Data;
using HrSystem.WebUI.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("HrSystemWebUIContextConnection") ??
    throw new InvalidOperationException("Connection string 'HrSystemWebUIContextConnection' not found.");

builder.Services.AddDbContext<HrSystemWebUIContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<HrSystemWebUIUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<HrSystemWebUIContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
