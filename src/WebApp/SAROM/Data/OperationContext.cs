using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SAROM.Models
{
    public class OperationContext : DbContext
    {
        public OperationContext (DbContextOptions<OperationContext> options)
            : base(options)
        {
        }

        public DbSet<SAROM.Models.OperationAction> OperationAction { get; set; }
    }
}
