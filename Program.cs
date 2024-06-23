using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IntegratedInventoryAndOrderManagementSystem.Data;
using IntegratedInventoryAndOrderManagementSystem.Models;
using IntegratedInventoryAndOrderManagementSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<ServiceBase<Product>, ServiceBase<Product>>();
builder.Services.AddScoped<ServiceBase<SalesOrder>, ServiceBase<SalesOrder>>();
builder.Services.AddScoped<ServiceBase<Location>, ServiceBase<Location>>();
builder.Services.AddScoped<ServiceBase<Customer>, ServiceBase<Customer>>();
builder.Services.AddScoped<ServiceBase<Status>, ServiceBase<Status>>();
builder.Services.AddScoped<ServiceBase<Shipment>, ServiceBase<Shipment>>();
builder.Services.AddScoped<ServiceBase<PurchaseOrder>, ServiceBase<PurchaseOrder>>();
builder.Services.AddScoped<ServiceBase<Employee>, ServiceBase<Employee>>();
builder.Services.AddScoped<ServiceBase<Inventory>, ServiceBase<Inventory>>();
builder.Services.AddScoped<ServiceBase<Vendor>, ServiceBase<Vendor>>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();

app.Run();
