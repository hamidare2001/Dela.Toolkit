using Dela.Toolkit.Application.Events;

namespace Dela.Toolkit.OutboxProcessor.DataStore;

public interface IDataStore
{
    void SetSubscriber(IDataStoreChangeTracker changeTracker);
    ISubscription SubscribeForChanges();
}