using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using VehicleWorkShop.Data;
using VehicleWorkShop.Service.Interface;
using VehicleWorkShop.Service.Repository;
using VehicleWorkShop.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<WorkShopDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("workshopdb")));

builder.Services.AddTransient<IWorkShop, WorkShopRP>();
builder.Services.AddTransient<ICategory, CategoryRP>();
builder.Services.AddTransient<IStockType, StockTypeRP>();
builder.Services.AddTransient<ISupplier,SupplierRP>();
builder.Services.AddTransient<IInventoryType, InventoryTypeRP>();
builder.Services.AddTransient<IProduct, ProductRP>();
builder.Services.AddTransient<IPurchase, PurchaseRP>();
builder.Services.AddTransient<IRole, RoleRP>();
builder.Services.AddTransient<IUser, UserRP>();
builder.Services.AddTransient<IStore, StoreRP>();
builder.Services.AddTransient<ICustomer, CustomerRP>();
builder.Services.AddTransient<ISale, SaleRP>();
builder.Services.AddTransient<IVehicleModel, VehicleModelRP>();

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


RotativaConfiguration.Setup(app.Environment.WebRootPath, "PurchaseInvoice"); 
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Home}/{id?}");

app.Run();
