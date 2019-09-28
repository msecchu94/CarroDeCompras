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

        string AddPedido = @"Insert into Pedidos 
                            (Fecha,Observacion,CodigoCliente) 
                            Values (@Fecha,@Observacion,(Select Codigo FROM Clientes WHERE IdUsuario=@IdUsuario)) ; SELECT CAST(scope_identity() AS int)";

        string AddDetallePedido = @"Insert into DetallesPedidos (CodigoProducto,Cantidad,PrecioUnitario,NumeroPedido,NumeroItem) Values (@CodigoProducto,@Cantidad,@PrecioUnitario,@NumeroPedido,@NumeroItem)";

        public bool AgregarPedido(PedidoBE pedidoBE)
        {
            var res = false;

            ConnectionString.Open();
            var trans = ConnectionString.BeginTransaction();


            try
            {

                pedidoBE.NumeroPedido = ConnectionString.ExecuteScalar<int>(AddPedido, param: pedidoBE, transaction: trans);

                foreach (var item in pedidoBE.ListaCarro)
                {
                    item.NumeroPedido = pedidoBE.NumeroPedido;
                }


                ConnectionString.Execute(AddDetallePedido, param: pedidoBE.ListaCarro, transaction: trans);
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

        public PedidoBE ObtenerPedidos(int id)
        {
            string getPedidos = @"Select p.[NumeroPedido],p.[CodigoCliente],p.[Fecha],p.[Observacion]

                              ,'split' as Split

                        ,d.[NumeroPedido],d.[NumeroItem],d.[CodigoProducto],d.[Cantidad],d.[PrecioUnitario]
                        FROM [Pedidos] p
                        INNER JOIN [DetallePedidos] d ON p.[NumeroPedido]=d.[NumeroPedido]
                        WHERE c.[CodigoCliente]=@id";

            try
            {
                PedidoBE pedidoBE =null;
                pedidoBE = ConnectionString.Query<PedidoBE, DetallePedidoBE, DetallePedidoBE>(getPedidos, (pedido, detalle) =>
                {
                    return new PedidoBE
                    {
                        NumeroPedido = pedido.NumeroPedido,
                        CodigoCliente = pedido.CodigoCliente,
                        Fecha = pedido.Fecha,
                        ListaCarro = detalle.
                    };

                }, param: new { IdUsuario = id }, splitOn: "Split");

                return pedidoBE;
            }

            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine(ObtenerCarro + "WHERE c.[IdUsuario]=@IdUsuario");
                throw;
            }

            throw new NotImplementedException();
        }
    }

}
