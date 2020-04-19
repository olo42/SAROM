using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Olo42.SAROM.DataAccess.Contracts;

namespace Olo42.SAROM.WebApp.Models
{
  public class OperationViewModel
  {
    private DateTime alertDateTime;
    
    public OperationViewModel()
    {
      this.OperationActions = new List<OperationAction>();
      this.Units = new List<Unit>();
    }

    [Display(Name = "Alarmierung")]
    public string Alerted 
    { 
      get 
      {
        var dateTimeString = $"{this.AlertDate} {this.AlertTime}";
        DateTime.TryParse(dateTimeString, out DateTime dateTime) ;
        return dateTime.ToString("dd.MM.yyyy HH:MM");
      }
    }

    [Display(Name = "Datum")]
    [DataType(DataType.Date)]
    [Required]
    public string AlertDate { get; set; }

    [Display(Name = "Uhrzeit")]
    [DataType(DataType.Time)]
    [Required]
    public string AlertTime { get; set; }

    [Display(Name = "Abschlussbericht")]
    public string ClosingReport { get; set; }

    [Display(Name = "Leitstelle")]
    public string Headquarter { get; set; }

    [Display(Name = "Kontakt Leitstelle")]
    public string HeadquarterContact { get; set; }

    public string Id { get; set; }

    public bool IsClosed { get; set; }

    public List<MissingPerson> MissingPeople { get; set; }

    public string Name { get; set; }

    [Display(Name = "Einsatznummer")]
    public string Number { get; set; }

    public List<OperationAction> OperationActions { get; set; }

    [Display(Name = "Einsatzleiter")]
    public string OperationLeader { get; set; }

    [Display(Name = "Kontakt Polizei")]
    public string PoliceContact { get; set; }

    [Display(Name = "Telefon Polizei")]
    public string PoliceContactPhone { get; set; }

    public List<Unit> Units { get; set; }
  }
}