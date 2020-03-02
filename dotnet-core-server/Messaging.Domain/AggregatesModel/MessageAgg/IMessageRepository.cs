using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Domain.AggregatesModel.MessageAgg
{
  public interface IMessageRepository
  {
    IUnitOfWork UnitOfWork { get; }
    Task Create(Message message);    
    Task<Message> GetAsync(Guid id);
  }
}
