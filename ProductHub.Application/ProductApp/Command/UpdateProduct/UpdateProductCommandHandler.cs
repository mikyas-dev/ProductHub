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


    public UpdateProductCommandHandler(IProductRepository productRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _productRepository = productRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
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



        product.Update(request.Name, request.Description, request.Price, CategoryId.Create(request.CategoryId), request.Quantity);
        await _productRepository.UpdateProductAsync(product);
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
        // rest of the code
    }

}