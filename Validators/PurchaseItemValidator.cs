using FluentValidation;
using inventorybackend.Api.DTOs.Purchase;

namespace inventorybackend.Api.Validators
{
    public class CreatePurchaseItemDtoValidator : AbstractValidator<CreatePurchaseItemDto>
    {
        public CreatePurchaseItemDtoValidator()
        {
            RuleFor(x => x.InventoryItemId)
                .GreaterThan(0)
                .WithMessage("Inventory item ID must be greater than 0");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0)
                .WithMessage("Unit price must be greater than 0");
        }
    }

    public class UpdatePurchaseItemDtoValidator : AbstractValidator<UpdatePurchaseItemDto>
    {
        public UpdatePurchaseItemDtoValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0)
                .WithMessage("Unit price must be greater than 0");
        }
    }
} 