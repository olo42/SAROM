using System;
using System.ComponentModel.DataAnnotations;

namespace SAROM.Models
{
  public class Document
  {
    public DateTime Created { get; set; }
    public string Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string OriginName { get; set; }
    [Required]
    public string Path { get; set; }
    [Required]
    public EDocumentType Type { get; set; }
  }
}