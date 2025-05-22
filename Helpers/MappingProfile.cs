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
            CreateMap<CreateInventoryDto, InventoryItem>()
                .ForMember(dest => dest.Sku, opt => opt.MapFrom(src => src.Sku))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.CostPrice, opt => opt.MapFrom(src => src.CostPrice))
                .ForMember(dest => dest.SellingPrice, opt => opt.MapFrom(src => src.SellingPrice))
                .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock))
                .ForMember(dest => dest.ReorderPoint, opt => opt.MapFrom(src => src.ReorderPoint))
                .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.SupplierId));

            CreateMap<UpdateInventoryDto, InventoryItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Sku, opt => opt.MapFrom(src => src.Sku))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.CostPrice, opt => opt.MapFrom(src => src.CostPrice))
                .ForMember(dest => dest.SellingPrice, opt => opt.MapFrom(src => src.SellingPrice))
                .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock))
                .ForMember(dest => dest.ReorderPoint, opt => opt.MapFrom(src => src.ReorderPoint))
                .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.SupplierId));

            CreateMap<InventoryItem, InventoryItemDto>()
                .ForMember(dest => dest.Sku, opt => opt.MapFrom(src => src.Sku))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.CostPrice))
                .ForMember(dest => dest.SellingPrice, opt => opt.MapFrom(src => src.SellingPrice))
                .ForMember(dest => dest.MinimumStockLevel, opt => opt.MapFrom(src => src.ReorderPoint))
                .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.SupplierId))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Name))
                .ForMember(dest => dest.SupplierContact, opt => opt.MapFrom(src => src.Supplier.ContactPerson));

            // Order mappings
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.InventoryItemName, opt => opt.MapFrom(src => src.InventoryItem.Name))
                .ForMember(dest => dest.InventoryItemSku, opt => opt.MapFrom(src => src.InventoryItem.Sku));

            CreateMap<CreateOrderDto, Order>();
            CreateMap<CreateOrderDto, OrderItem>();
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