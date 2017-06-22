using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyApp.Models
{
    public class PharmacyContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<InternationalName> InternationalNames { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Medicine> Medicines { get; set; }

        public PharmacyContext(DbContextOptions<PharmacyContext> options)
            : base(options)
        {
        }
    }
}
