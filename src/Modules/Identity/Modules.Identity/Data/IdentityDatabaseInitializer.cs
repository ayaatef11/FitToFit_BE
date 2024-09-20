using SharedKernal.Data;

namespace Modules.Identity.Data
{
    internal sealed class IdentityDatabaseInitializer(IdentityDbContext db) : IDatabaseInitializer
    {
        public async Task SeedAndCheckDefaultData(CancellationToken token = default)
        {
            await db.SeedUserTypesAsync(token);
        }
    }
}
