using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper.Configuration.Annotations;
using Olo42.SAROM.Logic.Operations;

namespace Olo42.SAROM.WebApp.Models
{
  public class OperationViewModel
  {
    public OperationViewModel()
    {
      this.OperationActions = new List<OperationActionViewModel>();
      this.Units = new List<UnitViewModel>();
      this.MissingPeople = new List<MissingPerson>();
    }

    [Display(Name = "Alarm")]
    [DataType(DataType.Date)]
    [SourceMember("AlertDateTime")]
    public DateTime Alert { get; set; }

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

    public List<OperationActionViewModel> OperationActions { get; set; }

    [Display(Name = "Einsatzleiter")]
    public string OperationLeader { get; set; }

    [Display(Name = "Kontakt Polizei")]
    public string PoliceContact { get; set; }

    [Display(Name = "Telefon Polizei")]
    public string PoliceContactPhone { get; set; }

    public List<UnitViewModel> Units { get; set; }

    public EStatus Status { get; set; }
  }
}