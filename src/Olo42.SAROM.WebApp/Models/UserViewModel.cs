using System.ComponentModel.DataAnnotations;

namespace Olo42.SAROM.WebApp.Models
{
    public class UserViewModel
  {
    [Required]
    [Display(Name = "Vorname")]
    public string FirstName { get; set; }
    
    [Required]
    [Display(Name = "Nachname")]
    public string LastName { get; set; }

    [Required]
    [Display(Name = "Benutzername")]
    public string LoginName { get; set; }
    
    [Required]
    [Display(Name = "Passwort")]
    public string Password { get; set; }

    [Required]
    [Display(Name = "Passwort überprüfen")]
    public string VerifyPassword { get; set; }  
  }
}