using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SharedKernal.Migrations
{
    public static class DbMigration
    {
        public static void CheckDatabaseMigration(this WebApplication app, params Type[] dbContextsTypes)
        {
            using (var scope = app.Services.CreateScope())
            {
                foreach (var dbContextType in dbContextsTypes)
                {
                    using var db = scope.ServiceProvider.GetRequiredService(dbContextType) as DbContext;
                    if (db.Database.GetPendingMigrations().Any())
                    {
                        db.Database.Migrate();
                    }
                }
            }
        }
    }
}
