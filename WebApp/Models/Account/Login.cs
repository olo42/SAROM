using System.ComponentModel.DataAnnotations;

namespace Olo42.SAROM.WebApp.Models.Account
{
    public class Login
  {
    [Required]
    [Display(Name = "Benutzername")]
    public string LoginName { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Passwort")]
    public string Password { get; set; }  

    public bool Failed { get; set; }  
  }
}