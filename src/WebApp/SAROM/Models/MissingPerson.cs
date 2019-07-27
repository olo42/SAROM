using System;
using System.ComponentModel.DataAnnotations;

namespace SAROM.Models
{
  public class MissingPerson
  {
    [Display(Name = "Erkrankungen")]
    public string Ailments { get; set; }

    [Display(Name = "Bekleidung")]
    public string Clothes { get; set; }

    [Display(Name = "Datum")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    [Display(Name = "Augenfarbe")]
    public string EyesColour { get; set; }

    [Display(Name = "Weitere Informationen")]
    public string FurtherInformation { get; set; }

    [Display(Name = "Geschlecht")]
    public string Gender { get; set; }

    [Display(Name = "Haarfarbe")]
    public string HairColor { get; set; }

    public string Id { get; set; }

    [Display(Name = "Bekannte Adressen")]
    public string KnownPlaces { get; set; }

    [Display(Name = "Medikamente")]
    public string Medications { get; set; }

    [Display(Name = "Vermisst seit")]
    public DateTime MissingSince { get; set; }

    [Display(Name = "Name")]
    public string Name { get; set; }

    public string OperationId { get; set; }

    [Display(Name = "Größe")]
    public string Size { get; set; }

    [Display(Name = "Hauttyp")]
    public string SkinType { get; set; }

    [Display(Name = "Besondere Merkmale")]
    public string SpecialCharacteristics { get; set; }

    [Display(Name = "Gewicht")]
    public string Weight { get; set; }
  }
}