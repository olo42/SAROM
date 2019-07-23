using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAROM.Models
{
  public class Operation
  {
    public Operation()
    {
      this.OperationActions = new List<OperationAction>();
      this.Units = new List<Unit>();
    }

    [DataType(DataType.Date)]
    public string AlertDate { get; set; }

    [DataType(DataType.Time)]
    public string AlertTime { get; set; }

    public string ClosingReport { get; set; }

    public string Id { get; set; }

    public bool IsClosed { get; set; }

    public string Name { get; set; }

    public List<OperationAction> OperationActions { get; set; }

    [DataType(DataType.Time)]
    public string State3 { get; set; }

    [DataType(DataType.Time)]
    public string State4 { get; set; }

    public List<Unit> Units { get; set; }
  }
}