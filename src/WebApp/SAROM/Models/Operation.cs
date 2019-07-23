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

    [Display(Name = "Datum")]
    [DataType(DataType.Date)]
    public string AlertDate { get; set; }

    [Display(Name="Einsatznummer")]
    public string Number { get; set; }

    [Display(Name="Leitstelle")]
    public string Headquarter { get; set; }

    [Display(Name = "Uhrzeit")]
    [DataType(DataType.Time)]
    public string AlertTime { get; set; }

    [Display(Name = "Abschlussbericht")]
    public string ClosingReport { get; set; }

    public string Id { get; set; }

    public bool IsClosed { get; set; }

    public string Name { get; set; }

    public List<OperationAction> OperationActions { get; set; }

    [Display(Name = "Status 3")]
    [DataType(DataType.Time)]
    public string State3 { get; set; }

    [Display(Name = "Status 4")]
    [DataType(DataType.Time)]
    public string State4 { get; set; }

    public List<Unit> Units { get; set; }
  }
}