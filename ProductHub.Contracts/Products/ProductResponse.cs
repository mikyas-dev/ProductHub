namespace ProductHub.Contracts.Products
{
    public record ProductResponse(
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
}