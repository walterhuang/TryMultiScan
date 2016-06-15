using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryMultiScan
{
    public class MultiScanContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
