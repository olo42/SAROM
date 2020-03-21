using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Olo42.SAROM.WebApp.Models
{
  public class Document
  {
    public Document()
    {
      Created = DateTime.Now;
    }

    public Document(IFormFile formFile)
    {
      Created = DateTime.Now;
      ContentType = formFile.ContentType;
      Extension = "jpg"; // TODO: Get Extension from Content-Type
      OriginName = formFile.FileName;
    }

    public string ContentType { get; set; }
    [Required]
    public DateTime Created { get; set; }
    public string Extension { get; set; }
    public string FullName => $"{Name}.{Extension}";
    public string Id { get; set; }
    public string Name
    {
      get
      {
        return $"{Created.Year}" +
          $"{Created.Month}" +
          $"{Created.Day}" +
          $"{Created.Hour}" +
          $"{Created.Minute}" +
          $"{Created.Second}";
      }
    }
    public string OriginName { get; set; }
    [Required]
    public EDocumentType Type { get; set; }
  }
}