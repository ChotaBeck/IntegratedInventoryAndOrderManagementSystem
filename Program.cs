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

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<ServiceBase<Product>, ServiceBase<Product>>();
builder.Services.AddScoped<ServiceBase<SalesOrder>, ServiceBase<SalesOrder>>();
builder.Services.AddScoped<ServiceBase<Location>, ServiceBase<Location>>();
builder.Services.AddScoped<ServiceBase<Customer>, ServiceBase<Customer>>();
builder.Services.AddScoped<ServiceBase<Status>, ServiceBase<Status>>();
builder.Services.AddScoped<ServiceBase<Shipment>, ServiceBase<Shipment>>();
builder.Services.AddScoped<ServiceBase<PurchaseOrder>, ServiceBase<PurchaseOrder>>();
builder.Services.AddScoped<ServiceBase<Inventory>, ServiceBase<Inventory>>();
builder.Services.AddScoped<ServiceBase<Vendor>, ServiceBase<Vendor>>();
builder.Services.AddScoped<ServiceBase<Department>, ServiceBase<Department>>();


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

// using(var scope = app.Services.CreateScope())
// {
//     var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

//     var roles = new[] {"Admin", "Order", "Customer", "Accounts","StockManager"};
//     foreach(var role in roles)
//     {
//         if(!roleManager.RoleExistsAsync(role).Result)
//         {
//             roleManager.CreateAsync(new IdentityRole(role)).Wait();
//         }
//     }
// }
// using (var scope = app.Services.CreateScope())
// {
//     var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

//     var usersToCreate = new List<(string Email, string Password, string Role)>
//     {
//         ("admin@admin.com", "Admin@123.", "Admin"),
//         ("order@order.com", "Order@123.", "Order"),
//         ("customer@customer.com", "Customer@123.", "Customer"),
//         ("accounts@accounts.com", "Accounts@123.", "Accounts"),
//         ("stockmanager@stockmanager.com", "StockManager@123.", "StockManager")
//     };

//     foreach (var (email, password, role) in usersToCreate)
//     {
//         try
//         {
//             if (await userManager.FindByEmailAsync(email) == null)
//             {
//                 var user = new IdentityUser
//                 {
//                     UserName = email,
//                     Email = email
//                 };
//                 var result = await userManager.CreateAsync(user, password);
//                 if (result.Succeeded)
//                 {
//                     await userManager.AddToRoleAsync(user, role);
//                 }
//                 else
//                 {
//                     // Handle user creation failure
//                     Console.WriteLine($"Failed to create user {email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
//                 }
//             }
//         }
//         catch (Exception ex)
//         {
//             // Handle exceptions
//             Console.WriteLine($"An error occurred while processing user {email}: {ex.Message}");
//         }
//     }
// }

app.Run();
