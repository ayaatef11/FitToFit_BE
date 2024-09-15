using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SharedKernal;
using SharedKernal.Presistance.DbConnection;

namespace Modules.Doctors.Data
{
    internal sealed class DoctorsDbConnectionFactory(IConfiguration configuration) : IDbConnectionFacory
    {
        public SqlConnection CreateConnection()
        {
            return new SqlConnection(configuration.GetConnectionString(SharedConstants.Settings.DatabaseConnection));
        }
    }
}
