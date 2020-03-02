using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messaging.Api.Models
{
  public class ResultType
  {
    public dynamic Data { get; set; }
    public bool IsSucceeded { get; set; }
    public string Message { get; set; }
  }
}
