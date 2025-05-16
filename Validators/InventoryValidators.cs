using FluentValidation;
using inventorybackend.Api.DTOs.Inventory;
namespace inventorybackend.Api.Validators
{
    using FluentValidation;
    using inventorybackend.Api.DTOs.Inventory;

    public class CreateInventoryDtoValidator : AbstractValidator<CreateInventoryDto>
    {
        public CreateInventoryDtoValidator()
        {
            RuleFor(x => x.SKU)
                .NotEmpty()
                .MaximumLength(50)
                .Matches("^[A-Z0-9-]+$").WithMessage("SKU must contain only uppercase letters, numbers, and hyphens");

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(500);

            RuleFor(x => x.CostPrice)
                .GreaterThan(0);

            RuleFor(x => x.SellingPrice)
                .GreaterThan(0)
                .GreaterThan(x => x.CostPrice)
                .WithMessage("Selling price must be greater than cost price");

            RuleFor(x => x.ReorderPoint)
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.SupplierId)
           .GreaterThan(0);
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