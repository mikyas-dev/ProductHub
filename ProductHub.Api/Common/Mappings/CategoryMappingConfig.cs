using Mapster;
using ProductHub.Application.CategoryApp.Command.Create;
using ProductHub.Application.CategoryApp.Command.Update;
using ProductHub.Contracts.Categories;

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

    }
}