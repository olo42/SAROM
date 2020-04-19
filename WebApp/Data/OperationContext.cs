using Microsoft.EntityFrameworkCore;

namespace Olo42.SAROM.WebApp.Models
{
  public class OperationContext : DbContext
  {
    public OperationContext(DbContextOptions<OperationContext> options)
        : base(options)
    {
    }

    public DbSet<Olo42.SAROM.WebApp.Models.Document> Document { get; set; }
    public DbSet<Olo42.SAROM.WebApp.Models.OperationViewModel> Operation { get; set; }
    public DbSet<Olo42.SAROM.WebApp.Models.OperationActionViewModel> OperationAction { get; set; }
    
    public DbSet<Olo42.SAROM.WebApp.Models.UnitViewModel> Unit { get; set; }

    public DbSet<Olo42.SAROM.WebApp.Models.MissingPerson> MissingPerson { get; set; }
  }
}