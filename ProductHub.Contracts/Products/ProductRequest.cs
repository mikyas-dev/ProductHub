namespace ProductHub.Contracts.Products
{
    public record ProductRequest(
        string Name,
        string Description,
        Guid CategoryId,
        decimal Price,
        int Quantity
    );

    public record UpdateProductRequest(
        string Name,
        string Description,
        Guid CategoryId,
        decimal Price,
        int Quantity,
        Guid Id
    );

    public record DeleteProductRequest(
        Guid Id
    );
}