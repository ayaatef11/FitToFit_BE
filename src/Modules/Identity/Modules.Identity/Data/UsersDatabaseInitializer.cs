using SharedKernal.Data;

namespace Modules.Identity.Data
{
    internal sealed class UsersDatabaseInitializer(UsersDbContext db) : IDatabaseInitializer
    {
        public async Task SeedAndCheckDefaultData(CancellationToken token = default)
        {
            await db.SeedUserTypesAsync(token);
        }
    }
}
