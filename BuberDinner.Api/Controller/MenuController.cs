using BuberDinner.Application.Menus.Commands.CreateMenu;
using BuberDinner.Contracts.Menus;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controller;


[Route("hosts/{hostId}/menus")]
public class MenuController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public MenuController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    [HttpPost]
    public async Task<IActionResult> CreateMunu(CreateMenuRequest request , string hostId)
    { 
        var Command = _mapper.Map<CreateMenuCommand>(request);

        var createMenuResult = await _mediator.Send(Command);
        return Ok(request);
    }
}