using FluentValidation;
using inventorybackend.Api.DTOs.Supplier;

namespace inventorybackend.Api.Validators
{
    public class CreateSupplierDtoValidator : AbstractValidator<CreateSupplierDto>
    {
        public CreateSupplierDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Name must not be empty and must not exceed 100 characters");

            RuleFor(x => x.ContactPerson)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Contact person must not be empty and must not exceed 100 characters");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100)
                .WithMessage("Email must be a valid email address and must not exceed 100 characters");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .Matches(@"^\+?[0-9\s\-\(\)]+$")
                .MaximumLength(20)
                .WithMessage("Phone must be a valid phone number and must not exceed 20 characters");

            RuleFor(x => x.Address)
                .NotEmpty()
                .MaximumLength(200)
                .WithMessage("Address must not be empty and must not exceed 200 characters");

            RuleFor(x => x.City)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("City must not be empty and must not exceed 100 characters");

            RuleFor(x => x.Country)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Country must not be empty and must not exceed 100 characters");

            RuleFor(x => x.PostalCode)
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("Postal code must not be empty and must not exceed 20 characters");
        }
    }

    public class UpdateSupplierDtoValidator : AbstractValidator<UpdateSupplierDto>
    {
        public UpdateSupplierDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Name must not be empty and must not exceed 100 characters");

            RuleFor(x => x.ContactPerson)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Contact person must not be empty and must not exceed 100 characters");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100)
                .WithMessage("Email must be a valid email address and must not exceed 100 characters");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .Matches(@"^\+?[0-9\s\-\(\)]+$")
                .MaximumLength(20)
                .WithMessage("Phone must be a valid phone number and must not exceed 20 characters");

            RuleFor(x => x.Address)
                .NotEmpty()
                .MaximumLength(200)
                .WithMessage("Address must not be empty and must not exceed 200 characters");

            RuleFor(x => x.City)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("City must not be empty and must not exceed 100 characters");

            RuleFor(x => x.Country)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Country must not be empty and must not exceed 100 characters");

            RuleFor(x => x.PostalCode)
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("Postal code must not be empty and must not exceed 20 characters");
        }
    }
} 