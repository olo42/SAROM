using System.Collections.Generic;

namespace SAROM.Models
{
  public class Unit
  {
    public string GroupLeader { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
    public string PagerNumber { get; set; }
    public List<Person> People { get; set; }
  }
}