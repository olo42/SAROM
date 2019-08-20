using System;
using System.ComponentModel.DataAnnotations;

namespace SAROM.Models
{
  public class OperationAction
  {
    public string Action { get; set; }
    [Required]
    public DateTime Created { get; set; }
    public string Id { get; set; }
    public string Message { get; set; }
    [Required]
    public string OperationId { get; set; }
    public string UnitName { get; set; }
  }
}