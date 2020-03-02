using MediatR;
using Messaging.Command;
using Messaging.Api.Models;
using Messaging.Query.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messaging.Api.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Messaging.Api.Controllers
{
  [ApiController]
  [Route("api/Messaging")]
  public class MessagingController : ControllerBase
  {
    private ILogger<MessagingController> _logger;
    private readonly IMediator _mediator;
    private IHubContext<MessagingHub> _messagingHub;

    public MessagingController(ILogger<MessagingController> logger, IMediator mediator, IHubContext<MessagingHub> messagingHub)
    {
      _logger = logger;
      _mediator = mediator;
      _messagingHub = messagingHub;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MessagesViewModel>>> Get()
    {      
      try
      {        
        return Ok(await _mediator.Send<IEnumerable<MessagesViewModel>>(new FetchAllMessagesQuery()));
      }
      catch(Exception ex)
      {
        _logger.LogError(ex, ex.Message);
        return StatusCode(StatusCodes.Status500InternalServerError, new { err = "some error occured!" });
      }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ResultType>> Add(MessageCreateCommand command)
    {
      var commandResult = new ResultType();
      try
      {
        MessageCreateCommandValidator validator = new MessageCreateCommandValidator();
        var result = validator.Validate(command);
        if (!result.IsValid)
        {
          commandResult.IsSucceeded = false;
          result.Errors.ToList().ForEach(x => commandResult.Message = commandResult.Message + x + ". ");

          return BadRequest(result.Errors);
        }


        commandResult.IsSucceeded = await _mediator.Send<bool>(command);
        await _messagingHub.Clients.All.SendAsync("ReceiveMessage", command.CreatedBy, command.MessageContent, DateTime.Now);



        return Ok(commandResult);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, ex.Message);
        return StatusCode(StatusCodes.Status500InternalServerError, new { err = "some error occured!" });
      }
    }

  }
}
