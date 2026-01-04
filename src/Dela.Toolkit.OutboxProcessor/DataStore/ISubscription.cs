namespace Dela.Toolkit.OutboxProcessor.DataStore;

public interface ISubscription : IDisposable
{
    void CancelSubscription();
}