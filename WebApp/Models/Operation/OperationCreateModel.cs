using System;
using System.ComponentModel.DataAnnotations;

namespace Olo42.SAROM.WebApp.Models
{
  public class OperationCreateModel
  {
    [Display(Name = "Datum")]
    [DataType(DataType.Date)]
    [Required]
    public string AlertDate { get; set; }

    [Display(Name = "Uhrzeit")]
    [DataType(DataType.Time)]
    [Required]
    public string AlertTime { get; set; }

    public string Name { get; set; }

    [Display(Name = "Einsatznummer")]
    public string Number { get; set; }

    public DateTime AlertDateTime
    {
      get
      { 
        DateTime.TryParse($"{AlertDate} {AlertTime}", out DateTime result);
        
        return result;
      }
    }
  }
}