using CarroDeComprasBLL.Interfaces;
using CarroDeComprasCommon.Entidad;
using CarroDeComprasDAL.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CarroDeComprasDAL.Implementaciones
{
    public class RepositoryPedido : IRepositoryPedido
    {
        private readonly IConnectionFactory _connectionFactory;

        public RepositoryPedido(IConnectionFactory ConnectionFactory)
        {
            this._connectionFactory = ConnectionFactory;
        }

        public bool AgregarPedido(PedidoBE pedidoBE)
        {
            IDbConnection conn = null;
            IDbTransaction trans = null;

            try
            {
                using (conn = _connectionFactory.CreateConnection(abierta: true))
                using (trans = conn.BeginTransaction())
                {
                    #region Query

                    string addPedido = @"Insert into Pedidos 
                            (Fecha,Observacion,CodigoCliente) 
                            Values (@Fecha,@Observacion,(Select Codigo FROM Clientes WHERE IdUsuario=@IdUsuario)) ; SELECT CAST(scope_identity() AS int)";

                    string addDetallePedido = @"Insert into DetallesPedidos (CodigoProducto,Cantidad,PrecioUnitario,NumeroPedido,NumeroItem) Values (@CodigoProducto,@Cantidad,@PrecioUnitario,@NumeroPedido,@NumeroItem)";

                    #endregion

                    pedidoBE.NumeroPedido = conn.ExecuteScalar<int>(addPedido, param: pedidoBE, transaction: trans);

                    foreach (var item in pedidoBE.DetallesPedido)
                    {
                        item.NumeroPedido = pedidoBE.NumeroPedido;
                    }

                    conn.Execute(addDetallePedido, param: pedidoBE.DetallesPedido, transaction: trans);
                    trans.Commit();
                    return true;
                }
            }
            catch (Exception)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }

                return false;
            }
            finally
            {
                if(conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        public IEnumerable<PedidoBE> ObtenerPedidos()
        {
            #region Query

            string getDetallesPedidos = @"Select 
                                dp.[CodigoProducto]
                                ,dp.[Cantidad]
                                ,dp.[PrecioUnitario]
                                ,dp.[NumeroPedido]
                                ,dp.[NumeroItem]

                                ,'split' as Split

                                ,p.[Codigo]
                                ,p.[Nombre]
                                ,p.[PrecioUnitario]
                                FROM [DetallesPedidos] dp
                                INNER JOIN Productos p  ON dp.[CodigoProducto]=p.Codigo";


            string getNumeroPedido = @"Select NumeroPedido,CodigoCliente,Fecha,Observacion FROM Pedidos ORDER BY Fecha DESC";

            #endregion

            using (var Connection = _connectionFactory.CreateConnection())
            {
                PedidoBE pedido = new PedidoBE();


                // obtener numeros pedidos por codigo cliente

                var getpedidos = Connection.Query<PedidoBE>(getNumeroPedido);

                ////obtener detalles por numero pedidos

                pedido.DetallesPedido = Connection.Query<DetallePedidoBE, ProductoBE, DetallePedidoBE>(getDetallesPedidos + " WHERE dp.[NumeroPedido] IN @NumeroPedido", (detail, product) =>
                {
                    return new DetallePedidoBE
                    {
                        ProductoBE = product,
                        Cantidad = detail.Cantidad,
                        NumeroItem = detail.NumeroItem,
                        NumeroPedido = detail.NumeroPedido

                    };

                }, param: new { NumeroPedido = getpedidos.Select(x => x.NumeroPedido) }, splitOn: "Split").ToList();

                foreach (var item in getpedidos)
                {

                    item.DetallesPedido = pedido.DetallesPedido.Where(d => d.NumeroPedido == item.NumeroPedido).ToList();

                }

                return getpedidos;
            }
        }

        public IEnumerable<PedidoBE> ObtenerPedidosXusuario(int idUsuario)
        {
            #region Query

            string getDetallesPedidos = @"Select 
                                dp.[CodigoProducto]
                                ,dp.[Cantidad]
                                ,dp.[PrecioUnitario]
                                ,dp.[NumeroPedido]
                                ,dp.[NumeroItem]

                                ,'split' as Split

                                ,p.[Codigo]
                                ,p.[Nombre]
                                ,p.[PrecioUnitario]
                                FROM [DetallesPedidos] dp
                                INNER JOIN Productos p  ON dp.[CodigoProducto]=p.Codigo";

            string getCodigoCliente = @"Select Codigo FROM Clientes WHERE IdUsuario=@IdUsuario";

            string getNumeroPedido = @"Select NumeroPedido,CodigoCliente,Fecha,Observacion FROM Pedidos WHERE CodigoCliente=@CodigoCliente ORDER BY Fecha DESC";

            #endregion

            using (var Connection = _connectionFactory.CreateConnection())
            {
                PedidoBE pedido = new PedidoBE();

                // Obtener codigo cliente 

                var getcodigoCliente = Connection.Query<int>(getCodigoCliente, param: new { IdUsuario = idUsuario });

                // obtener numeros pedidos por codigo cliente

                var getpedidos = Connection.Query<PedidoBE>(getNumeroPedido, param: new { CodigoCliente = getcodigoCliente });

                ////obtener detalles por numero pedidos

                pedido.DetallesPedido = Connection.Query<DetallePedidoBE, ProductoBE, DetallePedidoBE>(getDetallesPedidos + " WHERE dp.[NumeroPedido] IN @NumeroPedido", (detail, product) =>
                    {
                        return new DetallePedidoBE
                        {
                            ProductoBE = product,
                            Cantidad = detail.Cantidad,
                            NumeroItem = detail.NumeroItem,
                            NumeroPedido = detail.NumeroPedido,

                        };

                    }, param: new { NumeroPedido = getpedidos.Select(x => x.NumeroPedido) }, splitOn: "Split").ToList();

                foreach (var item in getpedidos)
                {

                    item.DetallesPedido = pedido.DetallesPedido.Where(d => d.NumeroPedido == item.NumeroPedido).ToList();

                }

                return getpedidos;
            }

        }

        public PedidoBE ObtenerPedido(int numPedido)
        {

            #region Query

            string getNumeroPedido = @"Select NumeroPedido,CodigoCliente,Fecha,Observacion FROM Pedidos WHERE NumeroPedido=@NumeroPedido ORDER BY Fecha ASC";

            string getDetallesPedidos = @"Select 
                                dp.[CodigoProducto]
                                ,dp.[Cantidad]
                                ,dp.[PrecioUnitario]
                                ,dp.[NumeroPedido]
                                ,dp.[NumeroItem]

                                ,'split' as Split

                                ,p.[Codigo]
                                ,p.[Nombre]
                                ,p.[PrecioUnitario]
                                FROM [DetallesPedidos] dp
                                INNER JOIN Productos p  ON dp.[CodigoProducto]=p.Codigo ";

            #endregion

            using (var Connection = _connectionFactory.CreateConnection())
            {
                PedidoBE pedido = new PedidoBE();

                var getpedido = Connection.Query<PedidoBE>(getNumeroPedido, param: new { NumeroPedido = numPedido });

                pedido.DetallesPedido = Connection.Query<DetallePedidoBE, ProductoBE, DetallePedidoBE>(getDetallesPedidos + " WHERE dp.[NumeroPedido] = @NumeroPedido", (detail, product) =>
                {
                    return new DetallePedidoBE
                    {
                        ProductoBE = product,
                        Cantidad = detail.Cantidad,
                        NumeroItem = detail.NumeroItem,
                        NumeroPedido = detail.NumeroPedido

                    };

                }, param: new { NumeroPedido = numPedido }, splitOn: "Split").ToList();

                foreach (var item in getpedido)
                {
                    item.DetallesPedido = pedido.DetallesPedido.Where(d => d.NumeroPedido == item.NumeroPedido);
                }

                pedido = getpedido.First();
                return pedido;
            }
        }
    }
}
