using ErrorOr;
using MediatR;
using ProductHub.Application.Common.Interfaces.Authentication;
using ProductHub.Application.Common.Interfaces.Persistence;
using ProductHub.Application.ProductApp.Common;

namespace ProductHub.Application.ProductApp.Query.AllProduct;

public class AllProductQueryHandler : IRequestHandler<AllProductQuery, ErrorOr<IEnumerable<ProductResult>>>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoriesRepository _categoryRepository;
    private readonly IUserRepository _userRepository;

    public AllProductQueryHandler(IProductRepository productRepository, ICategoriesRepository categoryRepository,
        IUserRepository userRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<IEnumerable<ProductResult>>> Handle(AllProductQuery request,
        CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetProductsAsync();

        var productResults = new List<ProductResult>();

        foreach (var product in products)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(product.CategoryId.Value);
            var user =  _userRepository.GetUserByIdAsync(product.UserId.Value);
            if (category == null || user == null)
            {
                continue;
            }

            productResults.Add(new ProductResult(
                product.Id.Value,
                product.Name, 
                category.Name, 
                category.Description,
                product.Description, 
                product.Price,
                product.Quantity, 
                product.IsAvailable,
                user.UserName));
        }

        return productResults;
    }
}