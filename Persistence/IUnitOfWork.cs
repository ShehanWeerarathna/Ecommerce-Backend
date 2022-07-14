namespace Ecommerce_Backend.Persistence;

public interface IUnitOfWork
{
    Task CompleteAsync();
}