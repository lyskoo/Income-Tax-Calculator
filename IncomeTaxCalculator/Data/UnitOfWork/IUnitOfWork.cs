using Data.Repositories.Interfaces;

namespace Data.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    ITaxBandRepository TaxBands {  get; }

    int Complete();
}
