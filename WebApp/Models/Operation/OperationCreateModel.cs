using System;
using System.ComponentModel.DataAnnotations;

namespace Olo42.SAROM.WebApp.Models
{
  public class OperationCreateModel
  {
    public string Name { get; set; }

    [Display(Name = "Einsatznummer")]
    public string Number { get; set; }

    [Display(Name = "Alarmierung")]
    [DataType(DataType.DateTime)]
    // [DisplayFormat(DataFormatString = "dd.MM.yyyy HH:mm", ApplyFormatInEditMode = true)]
    [Required]
    public DateTime AlertDateTime { get; set; }
  }
}