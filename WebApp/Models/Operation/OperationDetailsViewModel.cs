using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper.Configuration.Annotations;

namespace Olo42.SAROM.WebApp.Models
{
  public class OperationDetailsViewModel
  {
    public string Id { get; set; }

    public string Name { get; set; }

    [Display(Name = "Einsatznummer")]
    public string Number { get; set; }

    [Display(Name = "Alarmierung")]
    [SourceMember("AlertDateTime")]
    public DateTime AlertDateTime { get; set; }

    [Display(Name = "Leitstelle")]
    public string Headquarter { get; set; }

    [Display(Name = "Kontakt Leitstelle")]
    public string HeadquarterContact { get; set; }
    
    [Display(Name = "Kontakt Polizei")]
    public string PoliceContact { get; set; }

    [Display(Name = "Telefon Polizei")]
    public string PoliceContactPhone { get; set; }
    public List<UnitViewModel> Units { get; set; }
    public List<OperationActionViewModel> OperationActions { get; set; }

  }
}