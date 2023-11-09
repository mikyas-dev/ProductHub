using ErrorOr;
using MediatR;
using ProductHub.Application.Common.Interfaces.Authentication;
using ProductHub.Application.Common.Interfaces.Persistence;
using ProductHub.Application.ProductApp.Common;
using ProductHub.Domain.Category.ValueObjects;
using ProductHub.Domain.Common.Errors;
using ProductHub.Domain.User.ValueObjects;

namespace ProductHub.Application.ProductApp.Command.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ErrorOr<ProductResult>>
{
    private readonly IProductRepository _productRepository;

    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    private readonly IUserRepository _userRepository;

    private readonly ICategoriesRepository _categoriesRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository, IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository, ICategoriesRepository categoriesRepository)
    {
        _productRepository = productRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _categoriesRepository = categoriesRepository;
    }



    public async Task<ErrorOr<ProductResult>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductByIdAsync(request.Id);

        if (product == null)
        {
            return Errors.Product.ProductNotFound;
        }

        var userId = _jwtTokenGenerator.GetUserIdFromClaim();
        if (userId == null)
        {
            return Errors.User.UserNotFound;
        }

        if (product.UserId != UserId.Create(Guid.Parse(userId)))
        {
            return Errors.Product.ProductDonNotBelongToUser;
        }

        var category = await _categoriesRepository.GetCategoryByIdAsync(request.CategoryId);
        if (category == null)
        {
            return Errors.Category.CategoryNotFound;
        }

        var user = _userRepository.GetUserByIdAsync(Guid.Parse(userId));
        if (user == null)
        {
            return Errors.User.UserNotFound;
        }


        product.Update(request.Name, request.Description, request.Price, CategoryId.Create(request.CategoryId), request.Quantity);
        await _productRepository.UpdateProductAsync(product);
        return new ProductResult(
            product.Id.Value,
            product.Name,
            product.Description,
            category.Name,
            category.Description,
            product.Price,
            product.Quantity,
            product.IsAvailable,
            user.UserName
            );
        // rest of the code
    }

}