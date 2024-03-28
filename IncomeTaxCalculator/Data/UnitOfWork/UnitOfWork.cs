using Data.Data;
using Data.Repositories;
using Data.Repositories.Interfaces;

namespace Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly DBContext _context;

    public UnitOfWork(DBContext context)
    {
        _context = context;
        TaxBands = new TaxBandRepository(_context);
    }

    public ITaxBandRepository TaxBands { get; private set; }

    public int Complete()
    {
        return _context.SaveChanges();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _context.Dispose();
    }
}
