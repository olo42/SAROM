using System.ComponentModel.DataAnnotations;

namespace Olo42.SAROM.WebApp.Models.Account
{
    public class Login
  {
    [Required]
    [Display(Name = "Benutzername")]
    public string LoginName { get; set; }
    
    [Required]
    [Display(Name = "Passwort")]
    public string Password { get; set; }  

    public bool HasError 
    { 
      get { return string.IsNullOrEmpty(this.ErrorMessage) ? false : true; }  
    }

    public string ErrorMessage {get;set;}  
  }
}