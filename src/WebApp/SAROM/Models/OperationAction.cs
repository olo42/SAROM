using System;
using System.ComponentModel.DataAnnotations;

namespace SAROM.Models
{
  public class OperationAction
  {
    [Display(Name = "Maßnahme / Meldung")]
    public string Action { get; set; }
    [Required]
    [Display(Name = "Erstellt")]
    public DateTime Created { get; set; }
    public string Id { get; set; }
    [Display(Name = "Nachricht / Kommentar")]
    public string Message { get; set; }
    [Required]
    public string OperationId { get; set; }
    [Display(Name = "Einheit")]
    public string UnitName { get; set; }
  }
}