using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Olo42.SAROM.WebApp.Models
{
  public class Unit
  {
    [Display(Name = "Flächensucher")]
    public int AreaSeeker { get; set; }

    [Display(Name = "Trümmersuch-Teams")]
    public int DebrisSearcher { get; set; }

    [Display(Name = "Gruppenführer")]
    public string GroupLeader { get; set; }

    [Display(Name = "Helfer")]
    public int Helpers { get; set; }

    public string Id { get; set; }

    [Display(Name = "Mantrailer")]
    public int Mantrailer { get; set; }

    [Display(Name = "Name")]
    public string Name { get; set; }

    public string OperationId { get; set; }

    [Display(Name = "Funkrufname")]
    public string PagerNumber { get; set; }

    public List<Person> People { get; set; }

    [Display(Name = "Wasserorter")]
    public int WaterLocators { get; set; }
  }
}