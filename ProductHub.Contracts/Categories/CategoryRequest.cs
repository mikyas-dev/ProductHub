namespace ProductHub.Contracts.Categories
{
    public record CreateCategoryRequest(
        string Name,
        string Description
    );

    public record UpdateCategoryRequest(
        string Name,
        string Description
    );

    public record DeleteCategoryRequest(
        string Id
    );
}