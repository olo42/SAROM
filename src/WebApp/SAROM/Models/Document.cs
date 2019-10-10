using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace SAROM.Models
{
  public class Document
  {
    public Document() { }
    public Document(IFormFile formFile)
    {
      Created = DateTime.Now;
      Name = CreateDocumentName();
      Extension = "jpg"; // TODO: Get Extension from Content-Type
      OriginName = formFile.FileName;
    }
    public DateTime Created { get; set; }
    public string Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string OriginName { get; set; }
    [Required]
    public EDocumentType Type { get; set; }
    public string ContentType { get; set; }
    public string Extension { get; set; }

    public string FullName
    {
      get
      {
        return $"{Name}.{Extension}";
      }
    }

    private string CreateDocumentName()
    {
      return
          $"{Created.Year}" +
          $"{Created.Month}" +
          $"{Created.Day}" +
          $"{Created.Hour}" +
          $"{Created.Minute}" +
          $"{Created.Second}";
    }
  }
}