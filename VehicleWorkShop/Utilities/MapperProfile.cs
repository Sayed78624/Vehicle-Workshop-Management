using AutoMapper;
using VehicleWorkShop.Models;
using VehicleWorkShop.ViewModels;

namespace VehicleWorkShop.Utilities
{
    public class MapperProfile:Profile
    {
        public MapperProfile() 
        {
            CreateMap<WorkShop,WorkShopVM>().ReverseMap();
            CreateMap<Category, CategoryVM>().ReverseMap();
            CreateMap<StockType, StockTypeVM>().ReverseMap();
            CreateMap<Supplier, SupplierVM>().ReverseMap();    
            CreateMap<InventoryType, InventoryTypeVM>().ReverseMap();
            CreateMap<Product, ProductVM>().ReverseMap();
            CreateMap<Purchase, PurchaseVM>().ReverseMap();
            CreateMap<PurchaseDetail, PurchaseDetailVM>().ReverseMap();
            CreateMap<Role, RoleVM>().ReverseMap();   
            CreateMap<Users, UserVM>().ReverseMap();
            CreateMap<Store, StoreVM>().ReverseMap();

        }
    }
}
