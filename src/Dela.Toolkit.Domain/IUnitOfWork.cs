namespace Dela.Toolkit.Domain;

public interface IUnitOfWork:IDisposable
{
    Task<bool> CommitAsync();
}