using FluentValidation;
using inventorybackend.Api.DTOs.User;

namespace inventorybackend.Api.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Username must not be empty and must not exceed 50 characters");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100)
                .WithMessage("Email must be a valid email address and must not exceed 100 characters");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(100)
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one number")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("First name must not be empty and must not exceed 50 characters");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Last name must not be empty and must not exceed 50 characters");

            RuleFor(x => x.Role)
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("Role must not be empty and must not exceed 20 characters");
        }
    }

    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("First name must not be empty and must not exceed 50 characters");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Last name must not be empty and must not exceed 50 characters");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100)
                .WithMessage("Email must be a valid email address and must not exceed 100 characters");

            RuleFor(x => x.Password)
                .MaximumLength(100)
                .When(x => !string.IsNullOrEmpty(x.Password))
                .Matches("[A-Z]").When(x => !string.IsNullOrEmpty(x.Password))
                .WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").When(x => !string.IsNullOrEmpty(x.Password))
                .WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").When(x => !string.IsNullOrEmpty(x.Password))
                .WithMessage("Password must contain at least one number")
                .Matches("[^a-zA-Z0-9]").When(x => !string.IsNullOrEmpty(x.Password))
                .WithMessage("Password must contain at least one special character");

            RuleFor(x => x.Role)
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("Role must not be empty and must not exceed 20 characters");
        }
    }
} 