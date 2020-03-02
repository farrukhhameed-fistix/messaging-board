using AutoMapper;
//using Domain.AggregatesModel.MessageAgg;
using Messaging.Command;
using Messaging.Domain.AggregatesModel.MessageAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Command
{
  public class CommandProfile : Profile
  {
    public CommandProfile()
    {
      CreateMap<MessageCreateCommand, Message>()
      .ForCtorParam("messageContent", opt => opt.MapFrom(src => src.MessageContent));
      
      
      CreateMap<Message, MessageCreateCommand>()
        .ForMember(x => x.CreatedBy, m => m.MapFrom(y => y.CreatedBy))
        .ForMember(x => x.MessageContent, m => m.MapFrom(y => y.MessageContent));
    }
    
  }
}
