using Microsoft.EntityFrameworkCore;
using SAROM.Models;

namespace SAROM.Models
{
  public class OperationContext : DbContext
  {
    public OperationContext(DbContextOptions<OperationContext> options)
        : base(options)
    {
    }

    public DbSet<SAROM.Models.Operation> Operation { get; set; }
    public DbSet<SAROM.Models.OperationAction> OperationAction { get; set; }

    public DbSet<SAROM.Models.Person> Person { get; set; }

    public DbSet<SAROM.Models.Unit> Unit { get; set; }

    public DbSet<SAROM.Models.MissingPerson> MissingPerson { get; set; }
  }
}