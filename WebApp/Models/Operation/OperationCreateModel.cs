// Copyright (c) Oliver Appel. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.ComponentModel.DataAnnotations;

namespace Olo42.SAROM.WebApp.Models
{
  public class OperationCreateModel
  {
    [Required]
    public string Name { get; set; }

    [Display(Name = "Einsatznummer")]
    [Required]
    public string Number { get; set; }

    [Display(Name = "Alarmierung")]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "dd.MM.yyyy HH:mm", ApplyFormatInEditMode = true)]
    [Required]
    public DateTime AlertDateTime { get; set; }
  }
}