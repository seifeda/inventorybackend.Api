using AutoMapper;
using inventorybackend.Api.DTOs.Purchase;
using inventorybackend.Api.DTOs.Inventory;
using inventorybackend.Api.DTOs.Order;
using inventorybackend.Api.DTOs.Supplier;
using inventorybackend.Api.DTOs.User;
using inventorybackend.Api.Models;

namespace inventorybackend.Api.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // PurchaseItem mappings
            CreateMap<PurchaseItem, PurchaseItemDto>()
                .ForMember(dest => dest.InventoryItemName, opt => opt.MapFrom(src => src.InventoryItem.Name))
                .ForMember(dest => dest.InventoryItemSku, opt => opt.MapFrom(src => src.InventoryItem.Sku));

            CreateMap<CreatePurchaseItemDto, PurchaseItem>();
            CreateMap<UpdatePurchaseItemDto, PurchaseItem>();

            // Purchase mappings
            CreateMap<Purchase, PurchaseDto>()
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Name));

            CreateMap<CreatePurchaseDto, Purchase>();
            CreateMap<UpdatePurchaseDto, Purchase>();

            // InventoryItem mappings
            CreateMap<InventoryItem, InventoryItemDto>()
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Name))
                .ForMember(dest => dest.Sku, opt => opt.MapFrom(src => src.Sku))
                .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock));

            CreateMap<CreateInventoryItemDto, InventoryItem>();
            CreateMap<UpdateInventoryItemDto, InventoryItem>();

            // Order mappings
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.InventoryItemName, opt => opt.MapFrom(src => src.InventoryItem.Name))
                .ForMember(dest => dest.InventoryItemSku, opt => opt.MapFrom(src => src.InventoryItem.Sku));

            CreateMap<CreateOrderDto, Order>();
            CreateMap<CreateOrderItemDto, OrderItem>();
            CreateMap<UpdateOrderDto, Order>();

            // Supplier mappings
            CreateMap<Supplier, SupplierDto>();
            CreateMap<CreateSupplierDto, Supplier>();
            CreateMap<UpdateSupplierDto, Supplier>();

            // User mappings
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
        }
    }
} 