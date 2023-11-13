using ErrorOr;
using MediatR;
using ProductHub.Application.Common.Interfaces.Authentication;
using ProductHub.Application.Common.Interfaces.Persistence;
using ProductHub.Application.ProductApp.Common;
using ProductHub.Domain.Common.Errors;
using ProductHub.Domain.User.ValueObjects;

namespace ProductHub.Application.ProductApp.Query.AllProduct;

public class AllProductQueryHandler : IRequestHandler<AllProductQuery, ErrorOr<IEnumerable<ProductResult>>>
{
    private readonly IProductRepository _productRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AllProductQueryHandler(IProductRepository productRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _productRepository = productRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<IEnumerable<ProductResult>>> Handle(AllProductQuery request,
        CancellationToken cancellationToken)
    {
        
        var products = request.MyProducts
            ? await _productRepository.GetProductsByUserIdAsync(UserId.Create(request.UserId!.Value))
            : await _productRepository.GetProductsAsync();

        var productResults = new List<ProductResult>();

        foreach (var product in products)
        {
            productResults.Add(new ProductResult(
                product.Id.Value,
                product.Name,
                product.Description,
                product.Category.Name,
                product.Category.Description,
                product.Price,
                product.Quantity,
                product.IsAvailable,
                product.User.UserName
            ));
        }

        return productResults;
    }
}