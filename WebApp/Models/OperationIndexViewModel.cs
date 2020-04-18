using System;
using System.ComponentModel.DataAnnotations;
using Olo42.SAROM.DataAccess.Contracts;

namespace Olo42.SAROM.WebApp.Models
{
  public class OperationIndexViewModel
  {
    private DateTime alertDateTime;

    public string Id { get; set; }

    public string Name { get; set; }

    [Display(Name = "Einsatznummer")]
    public string Number { get; set; }

    [Display(Name = "Alarmierung")]
    public string Alerted
    {
      get
      {
        return this.alertDateTime.ToString("dd.MM.yyyy HH:MM");
      }
    }

    public static explicit operator OperationIndexViewModel(
      OperationFile operationFile)
    {
      var operationIndexViewModel = new OperationIndexViewModel();
      operationIndexViewModel.Id = operationFile.Id.ToString();
      operationIndexViewModel.Name = operationFile.Name;
      operationIndexViewModel.Number = operationFile.Number;
      operationIndexViewModel.alertDateTime = operationFile.Alert;

      return operationIndexViewModel;
    }
  }
}