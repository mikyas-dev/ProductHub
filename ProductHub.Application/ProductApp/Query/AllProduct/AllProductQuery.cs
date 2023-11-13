using ErrorOr;
using MediatR;
using ProductHub.Application.ProductApp.Common;
using ProductHub.Domain.User.ValueObjects;

namespace ProductHub.Application.ProductApp.Query.AllProduct;

public record AllProductQuery(
    bool MyProducts = false,
    UserId? UserId = null
) : IRequest<ErrorOr<IEnumerable<ProductResult>>>;
