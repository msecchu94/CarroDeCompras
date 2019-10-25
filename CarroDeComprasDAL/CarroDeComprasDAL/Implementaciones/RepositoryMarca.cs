using CarroDeComprasCommon.Entidad;
using CarroDeComprasDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using CarroDeComprasBLL.Interfaces;

namespace CarroDeComprasDAL.Implementaciones
{
    public class RepositoryMarca : IRepositoryMarca
    {
        private readonly IConnectionFactory _connectionFactory;

        #region Querys

        private const string sqlCargarMarcas = "Select ID,Nombre from Marcas";

        private readonly string sqlObtenerPorId = "Select * From Marcas WHERE Codigo=";

        #endregion

        public RepositoryMarca(IConnectionFactory connectionFactory)
        {
            this._connectionFactory = connectionFactory;
        }

        public IEnumerable<MarcaBE> CargarMarcas()
        {
            using (var conn = _connectionFactory.CreateConnection())
            {
                var lista = conn.Query<MarcaBE>(sqlCargarMarcas);
                return lista;
            }
        }

        public MarcaBE ObtenerPorId(int Codigo)
        {
            using (var conn = _connectionFactory.CreateConnection())
            {
                MarcaBE marcaBE = new MarcaBE();
                marcaBE = conn.QueryFirst<MarcaBE>(sqlObtenerPorId + Codigo);

                return marcaBE;
            }
        }
    }
}
