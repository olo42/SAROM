using System.IO;

namespace SAROM
{
  public class SAROMSettings
  {
    public string MissingPeoplePath { get; set; }
    public string OperationsPath { get; set; }
    public string[] PermittedFileUpoadExtensions { get; set; }
    public string ProgramDataProjectPath { get; set; }
    public string WWWRootFolderName { get; set; }
    public string WWWRootPath => Path.Combine(Directory.GetCurrentDirectory(), WWWRootFolderName);

    public string GetMissingPeoplePhysicalPath(string operationId, string missingPersonId)
    {
      var path = Path.Combine(
        this.WWWRootPath,
        this.OperationsPath,
        operationId,
        this.MissingPeoplePath,
        missingPersonId).ToString();

      return path;
    }

    public string GetMissingPeopleOnlinePath(string operationId, string missingPersonId)
    {
      var path = Path.Combine(
        "\\",
        this.OperationsPath,
        operationId,
        this.MissingPeoplePath,
        missingPersonId).ToString();

      return path;
    }
  }
}