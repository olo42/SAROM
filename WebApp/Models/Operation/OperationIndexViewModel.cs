using System;
using System.ComponentModel.DataAnnotations;
using Olo42.SAROM.Logic.Operations;

namespace Olo42.SAROM.WebApp.Models
{
  public class OperationIndexViewModel
  {
    public string Id { get; set; }

    public string Name { get; set; }

    [Display(Name = "Einsatznummer")]
    public string Number { get; set; }

    [Display(Name = "Alarmierung")]
    public DateTime Alert { get; set; }

    public EStatus Status { get; set; }
  }
}