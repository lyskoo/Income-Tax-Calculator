using Data.Entities;

namespace Data.Data;

public static class DataSeeder
{
    public static List<TaxBand> GetTaxBandsDefaultData()
    {
        return new(){
            new TaxBand { Id = new Guid("7b3e6b6a-9a58-4b41-a815-3828a9f23f28"), Name = "Tax Band A", LowerLimit = 0, UpperLimit = 5000, TaxRate = 0 },
            new TaxBand { Id = new Guid("e7751ebc-1b20-48e1-9d67-d9fc58d06c4d"), Name = "Tax Band B", LowerLimit = 5000, UpperLimit = 20000, TaxRate = 20 },
            new TaxBand { Id = new Guid("f824c88c-6ff7-4dc5-b376-32c0c3fb2ba8"), Name = "Tax Band C", LowerLimit = 20000, TaxRate = 40 }
        };
    }

}
