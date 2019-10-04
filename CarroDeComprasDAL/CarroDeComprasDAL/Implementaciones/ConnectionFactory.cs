using CarroDeComprasBLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarroDeComprasBLL.Implementaciones
{
    public class ConnectionFactory : IConnectionFactory
    {
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionTable"].ConnectionString);
        }
    }
}
