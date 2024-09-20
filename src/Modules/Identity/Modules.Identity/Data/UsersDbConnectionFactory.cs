using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SharedKernal;
using SharedKernal.Presistance.DbConnection;

namespace Modules.Identity.Data
{
    internal sealed class UsersDbConnectionFactory(IConfiguration configuration) : IDbConnectionFacory
    {
        public SqlConnection CreateConnection()
        {
            return new SqlConnection(configuration.GetConnectionString(SharedConstants.Settings.DatabaseConnection));
        }
    }
}
