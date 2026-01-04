using Dela.Toolkit.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dela.Toolkit.Persistence.EF;

public class BaseDbContext : DbContext
{
    public override int SaveChanges()
    {
        AddOutboxItemsToTransaction();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        AddOutboxItemsToTransaction();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AddOutboxItemsToTransaction();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        AddOutboxItemsToTransaction();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void AddOutboxItemsToTransaction()
    {
        var outboxItems = ChangeTracker
            .Entries()
            .Select(e => e.Entity)
            .OfType<IAggregateRoot>()
            .SelectMany(e => e.UncommittedEvents)
            .Select(OutboxItemFactory.CreateOutboxItem)
            .ToList();

        //base.AddRange(outboxItems);
        // foreach (var outboxItem in outboxItems)
        // {    if(outboxItem.EventType.Contains("add"))
        //        base.Add(outboxItem);
        //     else if(outboxItem.EventType.Contains("update"))
        //         base.Update(outboxItem);
        //     else if(outboxItem.EventType.Contains("delete"))
        //         base.Remove(outboxItem);
        // }
    }
}