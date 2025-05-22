using FluentValidation;
using inventorybackend.Api.DTOs.Inventory;

namespace inventorybackend.Api.Validators
{
    public class InventoryItemValidator : AbstractValidator<CreateInventoryDto>
    {
        public InventoryItemValidator()
        {
            RuleFor(x => x.Sku)
                .NotEmpty()
                .MaximumLength(50)
                .Matches("^[A-Za-z0-9-]+$")
                .WithMessage("SKU must contain only letters, numbers, and hyphens");

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(500);

            RuleFor(x => x.CostPrice)
                .GreaterThan(0)
                .WithMessage("Cost price must be greater than 0");

            RuleFor(x => x.SellingPrice)
                .GreaterThan(0)
                .WithMessage("Selling price must be greater than 0");

            RuleFor(x => x.QuantityInStock)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quantity must be greater than or equal to 0");

            RuleFor(x => x.ReorderPoint)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Reorder point must be greater than or equal to 0");
        }
    }

    public class InventoryAdjustmentDtoValidator : AbstractValidator<InventoryAdjustmentDto>
    {
        public InventoryAdjustmentDtoValidator()
        {
            RuleFor(x => x.InventoryId)
                .GreaterThan(0);

            RuleFor(x => x.Quantity)
                .GreaterThan(0);

            RuleFor(x => x.AdjustmentType)
                .NotEmpty()
                .Must(x => x == "Add" || x == "Remove")
                .WithMessage("Adjustment type must be either 'Add' or 'Remove'");

            RuleFor(x => x.Reason)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}