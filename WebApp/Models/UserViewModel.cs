using System;
using System.ComponentModel.DataAnnotations;
using Olo42.SAROM.DataAccess.Contracts;

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

    [Display(Name = "Name")]
    public string FullName 
    { 
      get { return $"{this.FirstName} {this.LastName}"; }
    }

    [Required]
    [Display(Name = "Benutzername")]
    public string LoginName { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    [MinLength(8)]
    [Display(Name = "Passwort")]
    public string Password { get; set; }

    [Required]
    [Compare("Password")]
    [DataType(DataType.Password)]
    [Display(Name = "Passwort überprüfen")]
    public string VerifyPassword { get; set; }

    public static explicit operator UserViewModel(User user)
    {
      if (user == null)
        throw new ArgumentNullException(nameof(user));

      var userViewModel = new UserViewModel();
      userViewModel.FirstName = user.FirstName;
      userViewModel.LastName = user.LastName;
      userViewModel.LoginName = user.LoginName;
      userViewModel.Password = user.Password;
      userViewModel.VerifyPassword = user.Password;

      return userViewModel;
    }  
  }
}