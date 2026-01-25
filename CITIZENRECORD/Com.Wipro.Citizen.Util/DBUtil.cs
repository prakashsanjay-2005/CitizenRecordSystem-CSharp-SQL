using Microsoft.Data.SqlClient;

namespace Com.Wipro.Citizen.Util
{
    public class DBUtil
    {
        public static string constr =
            "Data Source=localhost\\SQLEXPRESS;Initial Catalog=CITIZENDB;Integrated Security=True;TrustServerCertificate=True";

        public static SqlConnection GetDBConnection()
        {
            return new SqlConnection(constr);
        }
    }
}