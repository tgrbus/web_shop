using Grbus.WebShop.Application.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Grbus.WebShop.Infrastructure.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;

        private IDbContextTransaction? _transaction = null;
        
        public UnitOfWork(
            ApplicationDbContext context,
            ILogger<UnitOfWork> logger
            )
        {
            _context = context;
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

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
