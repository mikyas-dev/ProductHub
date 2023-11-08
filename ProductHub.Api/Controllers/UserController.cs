using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductHub.Application.Authentication.Commands.Roles;
using ProductHub.Contracts.Users;

namespace ProductHub.Api.Controllers;

[Route("user")]
[Authorize(Policy = "Admin")]
public class UserController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UserController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("role")]
    public async Task<IActionResult> AddRole(RoleRequest request)
    {
        var command = _mapper.Map<RoleCommand>(request);
        var result = await _mediator.Send(command);
        return result.Match(
            user => Ok(_mapper.Map<RoleResponse>(user)),
            error => Problem(error)
        );
    }
}
