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
        private readonly IConnectionFactory _connectionFactory;

        #region Querys

        private const string InsertCarro = @"INSERT INTO Carro(IdUsuario,Cantidad,CodigoProducto) 
                                           VALUES(@idUsuario,@cantidadProducto,@codigoProducto)";

        private const string GetCarro = @"SELECT   
                    c.[Cantidad] 
                    ,c.[IdUsuario]
                    ,c.[CodigoProducto]
                     
                    ,'split' as Split

                    ,p.[Codigo]
                    ,p.[Nombre]
                    ,p.[PrecioUnitario]
                    FROM [Carro] c
                    INNER JOIN Productos p ON c.[CodigoProducto] = p.Codigo";

        private const string deleteItem = @"DELETE FROM Carro WHERE CodigoProducto=@codigoProducto AND IdUsuario=@IdUsuario ";

        private const string deleteCarro = @"DELETE FROM Carro WHERE IdUsuario=@IdUsuario";

        private const string ActualizarCarro = @"UPDATE Carro SET Cantidad=@suma WHERE CodigoProducto=@_codigoProducto";

        private const string sqlComparar = @"Select COUNT (*) FROM Carro WHERE CodigoProducto=@_codigoProducto AND IdUsuario=@idUsuario";

        #endregion

        public RepositoryCarro(IConnectionFactory ConnectionFactory)
        {
            this._connectionFactory = ConnectionFactory;
        }

        public void AgragarCarro(int codigoProducto, int cantidadProducto, int idUsuario)
        {
            using (var Connection = _connectionFactory.CreateConnection())
            {
                var result = Connection.Execute(InsertCarro, param: new { idUsuario, cantidadProducto, codigoProducto });
            }
        }

        public PedidoBE ObtenerCarro(int IdUsuario)
        {
            using (var Connection = _connectionFactory.CreateConnection())
            {
                try
                {
                    PedidoBE pedidoBE = new PedidoBE();
                    pedidoBE.DetallesPedido = Connection.Query<CarroBE, ProductoBE, DetallePedidoBE>(GetCarro + " WHERE c.[IdUsuario]=@IdUsuario", (carro, producto) =>
                              {
                                  return new DetallePedidoBE
                                  {
                                      Cantidad = carro.Cantidad,
                                      ProductoBE = producto
                                  };

                              }, param: new { IdUsuario }, splitOn: "Split");

                    return pedidoBE;
                }

                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine(GetCarro + "WHERE c.[IdUsuario]=@IdUsuario");
                    throw;
                }
            }
        }

        public void EliminarItem(int codigoProducto, int IdUsuario)
        {
            using (var Connection = _connectionFactory.CreateConnection())
            {
                var Eliminar = Connection.Execute(deleteItem, param: new { codigoProducto, IdUsuario });
            }
        }

        public void VaciarCarro(int IdUsuario)
        {
            using (var Connection = _connectionFactory.CreateConnection())
            {
                Connection.Execute(deleteCarro, param: new { IdUsuario });
            }
        }

        public void ModificarCarro(int _codigoProducto, int suma)
        {
            using (var Connection = _connectionFactory.CreateConnection())
            {
                Connection.Execute(ActualizarCarro, param: new { _codigoProducto, suma });
            }
        }

        public bool CompararContenido(int _codigoProducto, int idUsuario)
        {
            using (var Connection = _connectionFactory.CreateConnection())
            {
                var result = Connection.ExecuteScalar<int>(sqlComparar, param: new { _codigoProducto, idUsuario });
                if (result != 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
