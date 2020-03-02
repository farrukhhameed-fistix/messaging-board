using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messaging.Command
{
  public class MessageCreateCommand : IRequest<bool>
  {
    public string CreatedBy { get; set; }
    public string MessageContent { get; set; }
  }

  public class MessageCreateCommandValidator : AbstractValidator<MessageCreateCommand>
  {
    public MessageCreateCommandValidator()
    {
      RuleFor(x => x.CreatedBy).NotEmpty();
      RuleFor(x => x.MessageContent).NotEmpty();
    }
  }

}
