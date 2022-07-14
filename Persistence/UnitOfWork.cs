using Ecommerce_Backend.Data;

namespace Ecommerce_Backend.Persistence;

public class UnitOfWork:IUnitOfWork
{
    private readonly DataContext _context;
    public UnitOfWork(DataContext context)
    {
        _context = context;
    }
    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}