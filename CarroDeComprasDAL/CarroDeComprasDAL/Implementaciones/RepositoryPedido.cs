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
    public class RepositoryPedido : IRepositoryPedido
    {

        public SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionTable"].ConnectionString);

        string addPedido = @"Insert into Pedidos 
                            (Fecha,Observacion,CodigoCliente) 
                            Values (@Fecha,@Observacion,(Select Codigo FROM Clientes WHERE IdUsuario=@IdUsuario)) ; SELECT CAST(scope_identity() AS int)";

        string addDetallePedido = @"Insert into DetallesPedidos (CodigoProducto,Cantidad,PrecioUnitario,NumeroPedido,NumeroItem) Values (@CodigoProducto,@Cantidad,@PrecioUnitario,@NumeroPedido,@NumeroItem)";

        public bool AgregarPedido(PedidoBE pedidoBE)
        {
            var res = false;

            ConnectionString.Open();
            var trans = ConnectionString.BeginTransaction();


            try
            {

                pedidoBE.NumeroPedido = ConnectionString.ExecuteScalar<int>(addPedido, param: pedidoBE, transaction: trans);

                foreach (var item in pedidoBE.DetallesPedido)
                {
                    item.NumeroPedido = pedidoBE.NumeroPedido;
                }


                ConnectionString.Execute(addDetallePedido, param: pedidoBE.DetallesPedido, transaction: trans);
                trans.Commit();
                res = true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                res = false;
            }
            finally
            {
                ConnectionString.Close();
            }

            return res;
        }

        public IEnumerable<PedidoBE> ObtenerPedidos(int idUsuario)
        {
            PedidoBE pedido = new PedidoBE();

            // ObtenerPedidos codigo cliente 

            string getCodigoCliente = @"Select Codigo FROM Clientes WHERE IdUsuario=@IdUsuario";
            var getcodigoCliente = ConnectionString.Query<int>(getCodigoCliente, param: new { IdUsuario = idUsuario });

            // obtener numeros pedidos por codigo cliente

            string getNumeroPedido = @"Select NumeroPedido,CodigoCliente,Fecha,Observacion FROM Pedidos WHERE CodigoCliente=@CodigoCliente";
            var getpedidos = ConnectionString.Query<PedidoBE>(getNumeroPedido, param: new { CodigoCliente = getcodigoCliente });

            ////obtener detalles por numero pedidos

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


            pedido.DetallesPedido = ConnectionString.Query<DetallePedidoBE, ProductoBE, DetallePedidoBE>(getDetallesPedidos + " WHERE dp.[NumeroPedido] IN @NumeroPedido", (detail, product) =>
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
}
