using SharedKernal.Data;

namespace Modules.Clients.Data
{
    internal sealed class ClientsDatabaseInitializer(ClientsDbContext db) : IDatabaseInitializer
    {
        public Task SeedAndCheckDefaultData(CancellationToken token = default)
        {
            return Task.CompletedTask;
        }
    }
}
