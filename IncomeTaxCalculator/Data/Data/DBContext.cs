using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Data;

public class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options)
       : base(options)
    {
    }

    public DbSet<TaxBand> TaxBands { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaxBand>().HasData(DataSeeder.GetTaxBandsDefaultData());
    }

}
