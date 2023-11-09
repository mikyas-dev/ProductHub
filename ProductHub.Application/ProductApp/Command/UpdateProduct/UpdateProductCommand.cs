using ErrorOr;
using MediatR;
using ProductHub.Application.ProductApp.Common;

namespace ProductHub.Application.ProductApp.Command.UpdateProduct;

public record UpdateProductCommand(
    string Name,
    string Description,
    Guid CategoryId,
    decimal Price,
    int Quantity,
    Guid Id
) : IRequest<ErrorOr<ProductResult>>;