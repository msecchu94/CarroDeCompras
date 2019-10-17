using CarroDeComprasBLL.Interfaces;
using CarroDeComprasCommon.Entidad;
using CarroDeComprasDAL.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarroDeComprasDAL.Implementaciones
{
    public class RepositoryCarro : IRepositoryCarro
    {
        //public SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionTable"].ConnectionString);

        private readonly IConnectionFactory _connectionFactory;

        public RepositoryCarro(IConnectionFactory ConnectionFactory)
        {
            this._connectionFactory = ConnectionFactory;
        }
        
        public void AgragarCarro(int codigoProducto, int cantidadProducto, int idUsuario)
        {
            string InsertCarro = @"INSERT INTO Carro(IdUsuario,Cantidad,CodigoProducto) VALUES(@idUsuario,@cantidadProducto,@codigoProducto)";

            using (var Connection = _connectionFactory.CreateConnection())
            {

                try
                {
                    var result = Connection.Execute(InsertCarro, param: new { idUsuario,cantidadProducto, codigoProducto });
                }
                catch (Exception ex)
                {
                    throw ex ;
                }
            }
        }

        public PedidoBE ObtenerCarro(int IdUsuario)
        {
            #region Query

            string ObtenerCarro = @"SELECT   
                    c.[Cantidad] 
                    ,c.[IdUsuario]
                    ,c.[CodigoProducto]
                     
                    ,'split' as Split

                    ,p.[Codigo]
                    ,p.[Nombre]
                    ,p.[PrecioUnitario]
                    FROM [Carro] c
                    INNER JOIN Productos p ON c.[CodigoProducto] = p.Codigo";

            #endregion

            using (var Connection = _connectionFactory.CreateConnection())
            {
                try
                {
                    PedidoBE pedidoBE = new PedidoBE();
                    pedidoBE.DetallesPedido = Connection.Query<CarroBE, ProductoBE, DetallePedidoBE>(ObtenerCarro + " WHERE c.[IdUsuario]=@IdUsuario",(carro, producto) =>
                              {
                                  return new DetallePedidoBE
                                  {
                                      Cantidad = carro.Cantidad,
                                      ProductoBE = producto
                                  };

                              }, param: new { IdUsuario = IdUsuario }, splitOn: "Split");

                    return pedidoBE;
                }

                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine(ObtenerCarro + "WHERE c.[IdUsuario]=@IdUsuario");
                    throw;
                }
            }
        }

        public void EliminarItem(int codigoProducto, int IdUsuario)
        {
            string EliminarItem = @"DELETE FROM Carro WHERE CodigoProducto=@codigoProducto AND IdUsuario=@IdUsuario ";

            using (var Connection = _connectionFactory.CreateConnection())
            {

                try
                {
                    var Eliminar = Connection.Execute(EliminarItem, param: new { codigoProducto, IdUsuario });
                }
                catch (Exception E)
                {
                    System.Diagnostics.Debug.WriteLine(EliminarItem + "DELETE FROM Carro WHERE CodigoProducto=@codigo");
                    throw E;
                }

            }
        }
        
        public void VaciarCarro(int IdUsuario)
        {
            string VaciarCarro = @"DELETE FROM Carro WHERE  IdUsuario=@IdUsuario";

            using (var Connection = _connectionFactory.CreateConnection())
            {
                try
                {
                    Connection.Execute(VaciarCarro, param: new { IdUsuario });
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
