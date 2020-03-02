using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Messaging.Api.Hubs
{
  public class MessagingHub : Hub
  {
    public async Task SendMessage(string message, string user, DateTime messageTime)
    {
      await Clients.All.SendAsync("ReceiveMessage", user, message, messageTime);
    }
  }
}
