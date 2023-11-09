using Mapster;
using ProductHub.Application.CategoryApp.Command.Create;
using ProductHub.Application.CategoryApp.Command.Update;
using ProductHub.Application.ProductApp.Command.UpdateProduct;
using ProductHub.Contracts.Categories;
using ProductHub.Contracts.Products;

namespace ProductHub.Api.Common.Mappings;

public class CategoryMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(UpdateCategoryRequest request, string id), UpdateCategoryCommand>()
            .Map(dest => dest.Id, src => Guid.Parse(src.id))
            .Map(dest => dest, src => src.request);
        
        config.NewConfig<CreateCategoryRequest, CreateCategoryCommand>()
            .Map(dest => dest, src => src);
        
        config .NewConfig<(UpdateProductRequest request, string id), UpdateProductCommand>()
            .Map(dest => dest.Id, src => Guid.Parse(src.id))
            .Map(dest => dest, src => src.request);
    }
}