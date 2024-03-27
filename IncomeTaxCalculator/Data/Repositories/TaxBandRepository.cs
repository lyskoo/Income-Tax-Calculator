using Data.Data;
using Data.Entities;
using Data.Repositories.Interfaces;

namespace Data.Repositories;

public class TaxBandRepository : Repository<TaxBand>, ITaxBandRepository
{
    public TaxBandRepository(DBContext dbContext) : base(dbContext)
    {
    }
}
