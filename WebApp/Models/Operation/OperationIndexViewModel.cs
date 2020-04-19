using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper.Configuration.Annotations;
using Olo42.SAROM.DataAccess.Contracts;

namespace Olo42.SAROM.WebApp.Models
{
  public class OperationIndexViewModel
  {
    public string Id { get; set; }

    public string Name { get; set; }

    [Display(Name = "Einsatznummer")]
    public string Number { get; set; }

    [Display(Name = "Alarmierung")]
    public string Alerted
    {
      get
      {
        return this.Alert.ToString("dd.MM.yyyy HH:MM");
      }
    }

    public DateTime Alert { get; set; }
  }
}