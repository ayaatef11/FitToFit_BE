using Microsoft.Data.SqlClient;

namespace SharedKernal.Presistance.DbConnection
{
    //why a class why not only an enum and store the dbconnection of all the modules in it ??
    public interface IDbConnectionFacory
    {
        public SqlConnection CreateConnection();
    }
}
