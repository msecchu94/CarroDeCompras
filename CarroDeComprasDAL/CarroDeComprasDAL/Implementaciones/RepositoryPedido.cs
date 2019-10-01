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

                foreach (var item in pedidoBE.ListaCarro)
                {
                    item.NumeroPedido = pedidoBE.NumeroPedido;
                }


                ConnectionString.Execute(addDetallePedido, param: pedidoBE.ListaCarro, transaction: trans);
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

        public List<PedidoBE> ObtenerPedidos(int id)
        {
            //PedidoBE detalle = new PedidoBE();


            // ObtenerPedidos codigo cliente 

            string getCodigoCliente = @"Select Codigo FROM Clientes WHERE IdUsuario=@IdUsuario";

            var codigoCliente = ConnectionString.Query<int>(getCodigoCliente, param: new { IdUsuario = id });


            // obtener numeros pedidos por codigo cliente

            string getNumeroPedido = @"Select NumeroPedido,CodigoCliente,Fecha,Observacion FROM Pedidos WHERE CodigoCliente=@CodigoCliente";

            var pedido = ConnectionString.Query<PedidoBE>(getNumeroPedido, param: new { CodigoCliente = codigoCliente });


            ////obtener detalles por numero pedidos

            string getDetallesPedidos = @"Select CodigoProducto,Cantidad,PrecioUnitario,NumeroPedido,NumeroItem FROM DetallesPedidos WHERE NumeroPedido IN @NumeroPedido";

           var detalle = ConnectionString.Query<DetallePedidoBE>(getDetallesPedidos, param: new { NumeroPedido = pedido.Select(x => x.NumeroPedido) }).ToList();

            List<PedidoBE> pedidoBE = new List<PedidoBE>();
            List<DetallePedidoBE> detallePedidoBE = new List<DetallePedidoBE>();

            foreach (var item in pedido)
            {
                pedidoBE.Add(item);

                foreach (var lista in detalle)
                {
                    if (item.NumeroPedido == lista.NumeroPedido)
                    {

                        detallePedidoBE.Add(lista);
                        pedidoBE.First().ListaCarro.Concat(detallePedidoBE);
                     
                    }

                }

            }
            return pedidoBE;
        }

    }
}
