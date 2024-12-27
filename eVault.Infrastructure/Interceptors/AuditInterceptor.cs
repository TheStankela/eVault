﻿using System.Text.Json;
using eVault.Domain.Attributes;
using eVault.Domain.Constants;
using eVault.Domain.Enums;
using eVault.Domain.Models;
using eVault.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace eVault.Infrastructure.Interceptors
{
    public sealed class AuditInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var dbContext = eventData.Context;

            if (dbContext == null)
                return base.SavingChangesAsync(eventData, result, cancellationToken);

            var entries = dbContext.ChangeTracker
                    .Entries<IAuditableEntity>()
                    .Where(_ => _.State != EntityState.Unchanged && 
                                _.State != EntityState.Detached);

            var auditEntries = new List<AuditEntry>();
            foreach (var entry in entries)
            {
                var auditAttribute = entry.Entity
                    .GetType()
                    .GetCustomAttributes(typeof(AuditAttribute), inherit: true)
                    .FirstOrDefault() as AuditAttribute;

                if (auditAttribute != null)
                {
                    var objectIdProperty = entry.Property("Id");
                    Guid objectId = objectIdProperty != null && objectIdProperty.CurrentValue is Guid guidValue
                        ? guidValue
                        : Guid.Empty;

                    if (objectId == Guid.Empty)
                        continue;

                    var auditEntry = new AuditEntry()
                    {
                        ObjectId = objectId,
                        EntityType = auditAttribute.EntityType,
                        UpdatedOn = DateTime.UtcNow,
                        Operation = entry.State switch
                        {
                            EntityState.Added => DatabaseOperation.Add,
                            EntityState.Modified => DatabaseOperation.Update,
                            EntityState.Deleted => DatabaseOperation.Delete,
                            _ => throw new NullReferenceException("Entity state cannot be null"),
                        }
                    };

                    if(entry.State == EntityState.Added)
                        entry.Entity.IsActive = true;
                    

                    if (entry.State == EntityState.Deleted)
                    {
                        entry.State = EntityState.Modified;
                        entry.Entity.IsActive = false;
                    }

                    foreach (var property in entry.Properties)
                    {
                        if(ApplicationResources.IgnoredAuditProperties.Contains(property.Metadata.Name))
                            continue;

                        if (auditEntry.Operation == DatabaseOperation.Update && string.Equals(property.OriginalValue?.ToString(), property.CurrentValue?.ToString()))
                            continue;

                        auditEntry.AuditChanges.Add(new AuditEntryChanges
                        {
                            PropertyName = property.Metadata.Name,
                            OldValue = auditEntry.Operation == DatabaseOperation.Add ? null : property.OriginalValue?.ToString(),
                            NewValue = auditEntry.Operation == DatabaseOperation.Delete ? null : property.CurrentValue?.ToString()
                        });
                    }

                    if (auditEntry.AuditChanges.Any())
                    {
                        auditEntry.Changes = JsonSerializer.Serialize(auditEntry.AuditChanges);
                        auditEntries.Add(auditEntry);
                    }
                }
            }

            if (auditEntries.Any())
                dbContext.Set<AuditEntry>().AddRange(auditEntries);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
