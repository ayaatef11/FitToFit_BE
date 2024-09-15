using Microsoft.Data.SqlClient;

namespace SharedKernal.Presistance.DbConnection
{
    public interface IDbConnectionFacory
    {
        public SqlConnection CreateConnection();
    }
}
