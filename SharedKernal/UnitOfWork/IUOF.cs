using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace SharedKernal.UnitOfWork
{
    public interface IUOF
    {
        DbConnection Connection { get; }
        void Begin();
        void Begin(IsolationLevel isolationLevel);
        Task Commit();
        void RegisterContext(DbContext context);
        Task Rollback();
        Task<T> UsingStrategy<T>(Func<Task<T>> action);
    }
}
