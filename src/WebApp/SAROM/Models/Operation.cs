﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SAROM.Models
{
  public class Operation
  {
    public Operation()
    {
      this.OperationActions = new List<OperationAction>();
      this.Units = new List<Unit>();
    }
    public string AlertDate { get; set; }
    public string AlertTime { get; set; }
    public string ClosingReport { get; set; }
    public string Id { get; set; }
    public bool IsClosed { get; set; }
    public string Name { get; set; }
    public List<OperationAction> OperationActions { get; set; }
    public string State3 { get; set; }
    public string State4 { get; set; }
    public List<Unit> Units { get; set; }
  }
}