namespace ProductHub.Application.ProductApp.Common;

public record ProductResult(
    Guid Id,
    string Name,
    string Description,
    string CategoryName,
    string CategoryDescription,
    decimal Price,
    int Quantity,
    bool IsAvailable,
    string ProductOwner
);