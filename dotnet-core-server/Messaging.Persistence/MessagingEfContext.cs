using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Persistence
{
  public class MessagingEfContext : DbContext, IUnitOfWork
  {
    public MessagingEfContext(DbContextOptions<MessagingEfContext> options)
            : base(options)
    {

    }

    public DbSet<Domain.AggregatesModel.MessageAgg.Message> Message { get; set; }
  }
}
