using FluentValidation;
using inventorybackend.Api.DTOs.Sale;

namespace inventorybackend.Api.Validators
{
    public class CreateSaleDtoValidator : AbstractValidator<CreateSaleDto>
    {
        public CreateSaleDtoValidator()
        {
            RuleFor(x => x.SaleDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Sale date cannot be in the future");

            RuleFor(x => x.CustomerName)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Customer name is required and must not exceed 100 characters");

            RuleFor(x => x.CustomerEmail)
                .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.CustomerEmail))
                .WithMessage("Invalid email format");

            RuleFor(x => x.CustomerPhone)
                .Matches(@"^\+?[0-9\s-]{10,}$")
                .When(x => !string.IsNullOrEmpty(x.CustomerPhone))
                .WithMessage("Invalid phone number format");

            RuleFor(x => x.PaymentMethod)
                .NotEmpty()
                .Must(x => new[] { "Cash", "Credit Card", "Debit Card", "Bank Transfer" }.Contains(x))
                .WithMessage("Invalid payment method");

            RuleFor(x => x.SaleItems)
                .NotEmpty()
                .WithMessage("At least one item is required for the sale");

            RuleForEach(x => x.SaleItems)
                .SetValidator(new CreateSaleItemDtoValidator());
        }
    }
} 