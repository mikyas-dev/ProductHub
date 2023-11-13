using ErrorOr;
using MediatR;
using ProductHub.Application.Common.Interfaces.Persistence;
using ProductHub.Application.ProductApp.Common;


namespace ProductHub.Application.ProductApp.Command.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ErrorOr<ProductResult>>
{
    private readonly IProductRepository _productRepository;
 

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
        
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

        return new ProductResult(
            product.Id.Value,
            product.Name,
            product.Description,
            product.Category.Name,
            product.Category.Description,
            product.Price,
            product.Quantity,
            product.IsAvailable,
            product.User.UserName
        );
    }
}

