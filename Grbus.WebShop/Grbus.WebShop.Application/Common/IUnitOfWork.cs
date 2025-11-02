namespace Grbus.WebShop.Application.Common
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        void CreateTransaction(System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.ReadCommitted);
        void CommitTransaction();
        void RollbackTransaction();
    }
}
