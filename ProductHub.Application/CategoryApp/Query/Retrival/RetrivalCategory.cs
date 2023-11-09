using ErrorOr;
using MediatR;
using ProductHub.Application.CategoryApp.Common;

namespace ProductHub.Application.CategoryApp.Query.Retrival;

public record RetrivalCategoryQuery() : IRequest<ErrorOr<List<CategoryResult>>>;