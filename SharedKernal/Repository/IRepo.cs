using Microsoft.EntityFrameworkCore;

namespace SharedKernal.Repository
{
    public interface IDbContext<TDbContext>
        where TDbContext : DbContext
    {
        TDbContext DbContext { get; }
    }
    public interface IRepo
    {
        Task Save();
    }
    public abstract class BaseRepo : IRepo
    {
        public async Task Save()
        {
            throw new NotImplementedException();
        }
    }
}
