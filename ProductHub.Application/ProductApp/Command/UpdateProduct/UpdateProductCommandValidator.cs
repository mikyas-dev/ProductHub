namespace ProductHub.Application.ProductApp.Command.UpdateProduct;
using FluentValidation;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(50)
            .WithMessage("Name must not exceed 50 characters");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required")
            .MaximumLength(500)
            .WithMessage("Description must not exceed 500 characters");

        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage("Category is required");

        RuleFor(x => x.Price)
            .NotEmpty()
            .WithMessage("Price is required")
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0");

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .WithMessage("Quantity is required")
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0");
    }
}