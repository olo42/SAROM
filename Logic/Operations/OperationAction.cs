using System;

namespace Olo42.SAROM.Logic.Operations
{
  public class OperationAction
  {
    public string Action { get; set; }
    public DateTime Created { get; set; }
    public string Id { get; set; }
    public string Message { get; set; }
    public string UnitName { get; set; }
    public string OperationId { get; set; }
  }
}