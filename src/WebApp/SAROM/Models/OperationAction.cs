using System;

namespace SAROM.Models
{
  public class OperationAction
  {
    public string Id { get; set; }
    public string Message { get; set; }
    public string OperationId { get; set; }
    public DateTime Created { get; set; }
  }
}