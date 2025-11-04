using Grbus.WebShop.Application.Common;
using Grbus.WebShop.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Grbus.WebShop.Infrastructure.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IDomainEventService _domainEventService;
        private readonly ILogger<UnitOfWork> _logger;

        private IDbContextTransaction? _transaction = null;
        
        public UnitOfWork(
            ApplicationDbContext context,
            IDomainEventService domainEventService,
            ILogger<UnitOfWork> logger
            )
        {
            _context = context;
            _domainEventService = domainEventService;
            _logger = logger;
        }

        public void CreateTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _transaction ??= _context.Database.BeginTransaction(isolationLevel);
        }

        public void CommitTransaction()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("No active transaction to commit.");
            }
            try
            {
                _transaction.Commit();
                _transaction = null;
            }
            catch(Exception ex)
            {
                _transaction?.Rollback();
                _transaction?.Dispose();
                _transaction = null;
                _logger.LogError(ex, "Error committing transaction");
                throw;
            }
        }        

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
            _transaction = null;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = 0;
            try
            {
                _transaction ??= _context.Database.BeginTransaction();
                var events = _context.ChangeTracker.Entries<IHasDomainEvent>()
                        .Select(e => e.Entity.DomainEvents)
                        .SelectMany(de => de)
                        .Where(de => !de.IsPublished)
                        .ToList();
                foreach (var domainEvent in events)
                {
                    await _domainEventService.Publish(domainEvent);
                    domainEvent.IsPublished = true;
                }

                result = await _context.SaveChangesAsync(cancellationToken);
                await _transaction.CommitAsync(cancellationToken);
                _transaction = null;
            }
            catch (Exception ex)
            {
                _transaction?.Rollback();
                _transaction?.Dispose();
                _transaction = null;
                _logger.LogError(ex, "Error saving changes");
                throw;
            }  
            
            return result;
        }
    }
}
