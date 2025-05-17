using FluentValidation;
using inventorybackend.Api.DTOs.Inventory;

namespace inventorybackend.Api.Validators
{
    public class CreateInventoryItemDtoValidator : AbstractValidator<CreateInventoryItemDto>
    {
        public CreateInventoryItemDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Name must not be empty and must not exceed 100 characters");

            RuleFor(x => x.Sku)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("SKU must not be empty and must not exceed 50 characters");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0)
                .WithMessage("Unit price must be greater than 0");

            RuleFor(x => x.QuantityInStock)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quantity in stock must be greater than or equal to 0");

            RuleFor(x => x.MinimumStockLevel)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Minimum stock level must be greater than or equal to 0");

            RuleFor(x => x.SupplierId)
                .GreaterThan(0)
                .WithMessage("Supplier ID must be greater than 0");
        }
    }

    public class UpdateInventoryItemDtoValidator : AbstractValidator<UpdateInventoryItemDto>
    {
        public UpdateInventoryItemDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Name must not be empty and must not exceed 100 characters");

            RuleFor(x => x.Sku)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("SKU must not be empty and must not exceed 50 characters");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0)
                .WithMessage("Unit price must be greater than 0");

            RuleFor(x => x.QuantityInStock)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quantity in stock must be greater than or equal to 0");

            RuleFor(x => x.MinimumStockLevel)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Minimum stock level must be greater than or equal to 0");

            RuleFor(x => x.SupplierId)
                .GreaterThan(0)
                .WithMessage("Supplier ID must be greater than 0");
        }
    }
} 