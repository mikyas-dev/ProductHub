using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductHub.Application.CategoryApp.Command.Create;
using ProductHub.Application.CategoryApp.Command.Delete;
using ProductHub.Application.CategoryApp.Command.Update;
using ProductHub.Application.CategoryApp.Query.Retrival;
using ProductHub.Contracts.Categories;

namespace ProductHub.Api.Controllers;

[Route("category")]
[Authorize(Policy = "Admin")]
public class CategoryController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CategoryController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
    {
        var command = _mapper.Map<CreateCategoryCommand>(request);
        var result = await _mediator.Send(command);
        return result.Match(
            category => Ok(_mapper.Map<CategoryResponse>(category)),
            error => Problem(error)
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(string id)
    {
        var command = new DeleteCategoryCommand(Guid.Parse(id));
        var result = await _mediator.Send(command);
        return result.Match(
            category => Ok("Category deleted successfully!"),
            error => Problem(error)
        );
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetCategories()
    {
        var query = new RetrivalCategoryQuery();
        var result = await _mediator.Send(query);
        return result.Match(
            categories => Ok(_mapper.Map<List<CategoryResponse>>(categories)),
            error => Problem(error)
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(string id, UpdateCategoryRequest request)
    {
        var command = _mapper.Map<UpdateCategoryCommand>((request , id));
        var result = await _mediator.Send(command);
        return result.Match(
            category => Ok(_mapper.Map<CategoryResponse>(category)),
            error => Problem(error)
        );
    }
}

