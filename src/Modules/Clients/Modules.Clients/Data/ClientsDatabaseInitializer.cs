using SharedKernal.Data;

namespace Modules.Clients.Data
{
    //This class implements the IDatabaseInitializer interface,
    //which probably defines the contract for database initialization
    //(e.g., seeding data, migrations, or checking for the presence of default data).
    internal sealed class ClientsDatabaseInitializer(ClientsDbContext db) : IDatabaseInitializer
    {
        public Task SeedAndCheckDefaultData(CancellationToken token = default)
        {
            //empty implementation right now 
            return Task.CompletedTask;
        }
    }
}
