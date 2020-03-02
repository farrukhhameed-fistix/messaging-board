using Domain;
using Messaging.Domain.AggregatesModel.MessageAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Persistence
{
  public class MessageRepository : IMessageRepository
  {
    MessagingEfContext _messagingEfContext = null;

    public IUnitOfWork UnitOfWork => _messagingEfContext;

    public MessageRepository(MessagingEfContext messagingEfContext)
    {
      _messagingEfContext = messagingEfContext;
    }
    public async Task Create(Message message)
    {
      await _messagingEfContext.Message.AddAsync(message);
    }    

    public async Task<Message> GetAsync(Guid id)
    {
      return await _messagingEfContext.Message.FindAsync(id);
    }
  }
}
