using Dela.Toolkit.Persistence;

namespace Dela.Toolkit.OutboxProcessor.DataStore;

public interface IDataStoreChangeTracker
{
    void ChangeDetected(List<OutboxItem> item);
}