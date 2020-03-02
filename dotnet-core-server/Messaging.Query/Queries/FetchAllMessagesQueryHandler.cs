using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Messaging.Query.Queries
{
  public class FetchAllMessagesQueryHandler : IRequestHandler<FetchAllMessagesQuery, IEnumerable<MessagesViewModel>>
  {
    private readonly IConfiguration _configuration = null;

    public FetchAllMessagesQueryHandler(IConfiguration configuration)
    {
      _configuration = configuration;
    }
    
    public async Task<IEnumerable<MessagesViewModel>> Handle(FetchAllMessagesQuery query, CancellationToken cancellationToken)
    {
      IEnumerable<MessagesViewModel> items = null;
      using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
      {
        items = await connection.QueryAsync<MessagesViewModel>(@"select * from message");
      }

      return items;
    }
  }
}
