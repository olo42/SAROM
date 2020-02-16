using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Olo42.SAROM.WebApp.Models
{
  public class MissingPerson : IValidatableObject
  {
    [Display(Name = "Erkrankungen")]
    public string Ailments { get; set; }

    [Display(Name = "Bekleidung")]
    public string Clothes { get; set; }

    [Display(Name = "Geburtsdatum")]
    public string DateOfBirth { get; set; }

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
    [DataType(DataType.DateTime)]
    [Required]
    public DateTime MissingSince { get; set; }

    [Required]
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

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (MissingSince >= DateTime.Now)
      {
        yield return new ValidationResult(
            $"MissingSince must be earlier than now.",
            new[] { "MissingSince" });
      }
    }
  }
}