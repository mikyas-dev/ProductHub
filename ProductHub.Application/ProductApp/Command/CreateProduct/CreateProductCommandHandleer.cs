using ErrorOr;
using MediatR;
using ProductHub.Application.Common.Interfaces.Persistence;
using ProductHub.Application.ProductApp.Common;
using ProductHub.Domain.Common.Errors;


namespace ProductHub.Application.ProductApp.Command.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ErrorOr<ProductResult>>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoriesRepository _categoryRepository;

    private readonly IUserRepository _userRepository;

    public CreateProductCommandHandler(IProductRepository productRepository, ICategoriesRepository categoryRepository, IUserRepository userRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<ProductResult>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Domain.Product.Product(
            Domain.Product.ValueObjects.ProductId.CreateUnique(),
            request.Name,
            request.Description,
            request.Price,
            Domain.Category.ValueObjects.CategoryId.Create(request.CategoryId),
            Domain.User.ValueObjects.UserId.Create(request.UserId),
            request.Quantity
        );

        await _productRepository.AddProductAsync(product);
        var user = _userRepository.GetUserByIdAsync(request.UserId);
        var category = _categoryRepository.GetCategoryByIdAsync(request.CategoryId);
        if (user == null)
        {
            return Errors.User.UserNotFound;
        }

        if (category.Result == null)
        {
            return Errors.Category.CategoryNotFound;
        }

        return new ProductResult(
            product.Id.Value,
            product.Name,
            product.Description,
            category.Result.Name,
            category.Result.Description,
            product.Price,
            product.Quantity,
            product.IsAvailable,
            user.UserName
        );
    }
}

