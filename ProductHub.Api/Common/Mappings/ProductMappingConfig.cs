using Mapster;
using ProductHub.Application.ProductApp.Command.CreateProduct;
using ProductHub.Application.ProductApp.Command.UpdateProduct;
using ProductHub.Contracts.Products;

namespace ProductHub.Api.Common.Mappings;

public class ProductMapingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(UpdateProductRequest request, string id), UpdateProductCommand>()
            .Map(dest => dest.Id, src => Guid.Parse(src.id))
            .Map(dest => dest, src => src.request);
        
        config.NewConfig<(CreateProductRequest request, string id), CreateProductCommand>()
            .Map(dest => dest, src => src.request)
            .Map(dest => dest.UserId, src => Guid.Parse(src.id));
    }
}