namespace Dela.Toolkit.Application.Events;

public interface ISubscription : IDisposable
{
    void UnSubscribe();
}