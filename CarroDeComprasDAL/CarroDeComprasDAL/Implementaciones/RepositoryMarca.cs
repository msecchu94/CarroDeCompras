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

namespace CarroDeComprasDAL.Implementaciones
{
    public class RepositoryMarca : IRepositoryMarca
    {
        public SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionTable"].ConnectionString);

        public IEnumerable<MarcaBE> CargarMarca()
        {
            try
            {
                string sql = "Select ID,Nombre from Marcas";
                var lista = ConnectionString.Query<MarcaBE>(sql);
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public MarcaBE ObtenerPorId(int Codigo)
        {
            try
            {
                string sql = "Select * From Marcas WHERE Codigo=" + Codigo;
                MarcaBE marcaBE = new MarcaBE();

                marcaBE = ConnectionString.QueryFirst<MarcaBE>(sql);

                return marcaBE;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
