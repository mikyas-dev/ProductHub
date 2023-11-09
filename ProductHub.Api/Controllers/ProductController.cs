using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductHub.Application.ProductApp.Command.CreateProduct;
using ProductHub.Application.ProductApp.Command.UpdateProduct;
using ProductHub.Application.ProductApp.Query.AllProduct;
using ProductHub.Contracts.Products;

namespace ProductHub.Api.Controllers;

[Route("product")]
[Authorize]

public class ProductController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductRequest request)
    {
        var command = _mapper.Map<CreateProductCommand>(request);
        var result = await _mediator.Send(command);
        return result.Match(
            product => Ok(_mapper.Map<ProductResponse>(product)),
            error => Problem(error)
        );
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetProducts()
    {
        var query = new AllProductQuery();
        var result = await _mediator.Send(query);
        return result.Match(
            products => Ok(_mapper.Map<List<ProductResponse>>(products)),
            error => Problem(error)
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(string id, UpdateProductRequest request)
    {
        var command = _mapper.Map<UpdateProductCommand>((id, request));
        var result = await _mediator.Send(command);
        return result.Match(
            product => Ok(_mapper.Map<ProductResponse>(product)),
            error => Problem(error)
        );
    }
}