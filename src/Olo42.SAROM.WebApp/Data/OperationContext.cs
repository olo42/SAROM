using Microsoft.EntityFrameworkCore;
using Olo42.SAROM.WebApp.Models;

namespace Olo42.SAROM.WebApp.Models
{
  public class OperationContext : DbContext
  {
    public OperationContext(DbContextOptions<OperationContext> options)
        : base(options)
    {
    }

    public DbSet<Olo42.SAROM.WebApp.Models.Operation> Operation { get; set; }
    public DbSet<Olo42.SAROM.WebApp.Models.OperationAction> OperationAction { get; set; }

    public DbSet<Olo42.SAROM.WebApp.Models.Person> Person { get; set; }

    public DbSet<Olo42.SAROM.WebApp.Models.Unit> Unit { get; set; }

    public DbSet<Olo42.SAROM.WebApp.Models.MissingPerson> MissingPerson { get; set; }
  }
}