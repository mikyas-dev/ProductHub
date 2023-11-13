using System.IdentityModel.Tokens.Jwt;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductHub.Application.ProductApp.Command.CreateProduct;
using ProductHub.Application.ProductApp.Command.UpdateProduct;
using ProductHub.Application.ProductApp.Query.AllProduct;
using ProductHub.Contracts.Products;
using ProductHub.Domain.User.ValueObjects;

namespace ProductHub.Api.Controllers;

[Route("product")]
[Authorize]

public class ProductController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();

    public ProductController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductRequest request)
    {
        var userId = _jwtSecurityTokenHandler.ReadJwtToken(Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;
        Console.WriteLine(userId);
        if (userId is null)
        {
            return Unauthorized();
        }
        var command = _mapper.Map<CreateProductCommand>((request, userId));
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

    [HttpGet("my-products")]
    public async Task<IActionResult> GetMyProducts()
    {
        var userId = _jwtSecurityTokenHandler.ReadJwtToken(Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;
        if (userId is null)
        {
            return Unauthorized();
        }
        var query = new AllProductQuery(true, UserId.Create(Guid.Parse(userId)));
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