using FluentValidation;
using inventorybackend.Api.DTOs.Sale;

namespace inventorybackend.Api.Validators
{
    public class CreateSaleItemDtoValidator : AbstractValidator<CreateSaleItemDto>
    {
        public CreateSaleItemDtoValidator()
        {
            RuleFor(x => x.InventoryItemId)
                .GreaterThan(0)
                .WithMessage("Invalid inventory item ID");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0)
                .WithMessage("Unit price must be greater than 0");

            RuleFor(x => x.Notes)
                .MaximumLength(500)
                .When(x => !string.IsNullOrEmpty(x.Notes))
                .WithMessage("Notes must not exceed 500 characters");
        }
    }
} 