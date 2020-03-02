using AutoMapper;
//using Domain.AggregatesModel.MessageAgg;
using MediatR;
using Messaging.Command;
using Messaging.Domain.AggregatesModel.MessageAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Messaging.Command
{
  public class MessageCreateCommandHandler: IRequestHandler<MessageCreateCommand, bool>
  {
    private readonly IMapper _mapper = null;
    private readonly IMessageRepository _messageRepository = null;

    public MessageCreateCommandHandler(IMessageRepository messageRepository, IMapper mapper)
    {
      _messageRepository = messageRepository;
      _mapper = mapper;
    }

    public async Task<bool> Handle(MessageCreateCommand command, CancellationToken cancellationToken)
    {

      Message message = _mapper.Map<MessageCreateCommand, Message>(command);

      await _messageRepository.Create(message);

      var response = await _messageRepository.UnitOfWork.SaveChangesAsync();      

      if (response > 0) return true;

      return false;
    }
  }
}
