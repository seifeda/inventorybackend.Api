using FluentValidation;
using inventorybackend.Api.DTOs.Order;

namespace inventorybackend.Api.Validators
{
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("User ID must be greater than 0");

            RuleFor(x => x.OrderItems)
                .NotEmpty()
                .WithMessage("Order must contain at least one item");

            RuleForEach(x => x.OrderItems)
                .SetValidator(new CreateOrderItemDtoValidator());
        }
    }

    public class CreateOrderItemDtoValidator : AbstractValidator<CreateOrderItemDto>
    {
        public CreateOrderItemDtoValidator()
        {
            RuleFor(x => x.InventoryItemId)
                .GreaterThan(0)
                .WithMessage("Inventory item ID must be greater than 0");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0");
        }
    }

    public class UpdateOrderDtoValidator : AbstractValidator<UpdateOrderDto>
    {
        public UpdateOrderDtoValidator()
        {
            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("Status is required")
                .Must(status => new[] { "Pending", "Processing", "Shipped", "Delivered", "Cancelled" }.Contains(status))
                .WithMessage("Status must be one of: Pending, Processing, Shipped, Delivered, Cancelled");
        }
    }
} 