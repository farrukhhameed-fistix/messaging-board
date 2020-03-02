using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Query.Queries
{
  public class FetchAllMessagesQuery : IRequest<IEnumerable<MessagesViewModel>>
  {
  }
}
