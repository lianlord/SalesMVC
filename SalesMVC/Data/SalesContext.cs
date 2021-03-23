using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesMVC.Models;

namespace SalesMVC.Data
{
    public class SalesContext : DbContext
    {
        public SalesContext (DbContextOptions<SalesContext> options)
            : base(options)
        {
        }

        public DbSet<SalesMVC.Models.Department> Department { get; set; }

        public DbSet<SalesMVC.Models.Seller> Seller { get; set; }
        public DbSet<SalesMVC.Models.SalesRecord> SalesRecord { get; set; }
    }
}
