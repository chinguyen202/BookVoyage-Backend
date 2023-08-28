using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using BookVoyage.Domain.Common;

namespace BookVoyage.Persistence.Data;

public class TimeStampInterceptor: SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var addedEntries = eventData.Context!.ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
        foreach (var trackEntry in addedEntries)
        {
            if(trackEntry.Entity is AuditableBaseEntity entity)
            {
                entity.CreatedAt = DateTime.Now;
                entity.ModifiedAt = DateTime.Now;
            }
        }

        var updatedEntries = eventData.Context.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);
        foreach (var trackEntry in updatedEntries)
        {
            if (trackEntry.Entity is AuditableBaseEntity entity)
            {
                entity.ModifiedAt = DateTime.Now;
            }
        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

}