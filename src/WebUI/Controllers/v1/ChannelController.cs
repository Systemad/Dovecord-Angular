using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using WebUI.Domain.Channels.Features;
using WebUI.Dtos.Channel;

namespace WebUI.Controllers.v1;

[Authorize]
[RequiredScope("API.Access")]
[ApiController]
[Route("api/channels")]
[ApiVersion("1.0")]
public class ChannelController : ControllerBase
{
    private readonly ILogger<ChannelController> _logger;
    private readonly IMediator _mediator;
    
    public ChannelController(ILogger<ChannelController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }   

    [ProducesResponseType(typeof(IEnumerable<ChannelDto>), 200)]
    [Produces("application/json")]
    [HttpGet(Name = "GetChannels")]
    public async Task<IActionResult> GetChannels()
    {
        var query = new GetChannelList.ChannelListQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [ProducesResponseType(typeof(ChannelDto), 200)]
    [Produces("application/json")]
    [HttpGet("{id:guid}", Name = "GetChannel")]
    public async Task<IActionResult> GetChannel(Guid id)
    {
        var query = new GetChannel.ChannelQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [ProducesResponseType(typeof(ChannelDto), 201)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost(Name = "AddChannel")]
    public async Task<IActionResult> AddChannel([FromBody] ChannelManipulationDto channelForCreation)
    {
        var command = new AddChannel.AddChannelCommand(channelForCreation);
        var commandResponse = await _mediator.Send(command);
        return Ok(commandResponse);
    }
    
    [Produces("application/json")]
    [HttpDelete("{id:guid}", Name = "DeleteChannel")]
    public async Task<IActionResult> DeleteChannel(Guid id)
    {
        var command = new DeleteChannel.DeleteChannelCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
    
    [Produces("application/json")]
    [HttpPut("{id:guid}", Name = "UpdateChannel")]
    public async Task<ActionResult> UpdateChannel(Guid id, ChannelManipulationDto channel)
    {
        var command = new UpdateChannel.UpdateChannelCommand(id, channel);
        await _mediator.Send(command);
        return NoContent();
    }
}