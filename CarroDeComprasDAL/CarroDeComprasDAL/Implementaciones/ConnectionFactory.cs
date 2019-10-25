using CarroDeComprasBLL.Interfaces;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CarroDeComprasBLL.Implementaciones
{
    public class ConnectionFactory : IConnectionFactory
    {
        public IDbConnection CreateConnection(bool abierta = false)
        {
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionTable"].ConnectionString);
            if (abierta) conn.Open();
            return conn;
        }
    }
}
