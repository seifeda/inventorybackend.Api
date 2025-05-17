using FluentValidation;
using inventorybackend.Api.DTOs.Purchase;

namespace inventorybackend.Api.Validators
{
    public class CreatePurchaseDtoValidator : AbstractValidator<CreatePurchaseDto>
    {
        public CreatePurchaseDtoValidator()
        {
            RuleFor(x => x.SupplierId)
                .GreaterThan(0)
                .WithMessage("Supplier ID must be greater than 0");

            RuleFor(x => x.PurchaseDate)
                .NotEmpty()
                .WithMessage("Purchase date is required");

            RuleFor(x => x.PurchaseItems)
                .NotEmpty()
                .WithMessage("At least one purchase item is required");

            RuleForEach(x => x.PurchaseItems)
                .SetValidator(new CreatePurchaseItemDtoValidator());
        }
    }

    public class UpdatePurchaseDtoValidator : AbstractValidator<UpdatePurchaseDto>
    {
        public UpdatePurchaseDtoValidator()
        {
            RuleFor(x => x.SupplierId)
                .GreaterThan(0)
                .WithMessage("Supplier ID must be greater than 0");

            RuleFor(x => x.PurchaseDate)
                .NotEmpty()
                .WithMessage("Purchase date is required");
        }
    }
} 