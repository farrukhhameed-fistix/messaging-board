using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Domain.AggregatesModel.MessageAgg
{
  public class Message : Entity
  {
    public Message(string createdBy, string messageContent)
    {
      CreatedBy = createdBy;
      MessageContent = messageContent;
      CreatedOn = DateTime.Now;
    }

    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string MessageContent { get; set; }
    
  }
}
