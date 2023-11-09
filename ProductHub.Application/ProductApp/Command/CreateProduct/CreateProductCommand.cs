using ErrorOr;
using MediatR;
using ProductHub.Application.ProductApp.Common;

namespace ProductHub.Application.ProductApp.Command.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    Guid CategoryId,
    Guid UserId,
    decimal Price,
    int Quantity
) : IRequest<ErrorOr<ProductResult>>;

