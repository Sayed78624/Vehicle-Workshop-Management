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
            CreateMap<Customer, CustomerVM>().ReverseMap();
            CreateMap<Sale, SaleVM>().ReverseMap();
            CreateMap<SaleDetails, SaleDetailVM>().ReverseMap();

            CreateMap<VehicleModel, VehicleModelVM>().ReverseMap();
            CreateMap<Transfer, TransferVM>()
             .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.TransferDetails));
            CreateMap<TransferDetail, TransferDetailVM>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.ProductName : string.Empty))
                .ForMember(dest => dest.SourceStoreName, opt => opt.MapFrom(src => src.SourceStore != null ? src.SourceStore.Name : string.Empty))
                .ForMember(dest => dest.DestinationStoreName, opt => opt.MapFrom(src => src.DestinationStore != null ? src.DestinationStore.Name : string.Empty));



        }
    }
}
