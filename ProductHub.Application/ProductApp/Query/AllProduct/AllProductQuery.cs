using ErrorOr;
using MediatR;
using ProductHub.Application.ProductApp.Common;

namespace ProductHub.Application.ProductApp.Query.AllProduct;

public record AllProductQuery : IRequest<ErrorOr<IEnumerable<ProductResult>>>;