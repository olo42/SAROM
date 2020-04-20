using System.ComponentModel.DataAnnotations;
using Olo42.SAROM.DataAccess.Contracts;

namespace Olo42.SAROM.WebApp.Models
{
  public class OperationEditModel
  {
    [Display(Name = "Abschlussbericht")]
    public string ClosingReport { get; set; }

    [Display(Name = "Leitstelle")]
    public string Headquarter { get; set; }

    [Display(Name = "Kontakt Leitstelle")]
    public string HeadquarterContact { get; set; }

    public string Id { get; set; }

    public bool IsClosed { get; set; }

    public EStatus Status { get; set; }

    public string Name { get; set; }

    [Display(Name = "Einsatznummer")]
    public string Number { get; set; }

    [Display(Name = "Einsatzleiter")]
    public string OperationLeader { get; set; }

    [Display(Name = "Kontakt Polizei")]
    public string PoliceContact { get; set; }

    [Display(Name = "Telefon Polizei")]
    public string PoliceContactPhone { get; set; }
  }
}