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
            PedidoBE pedidoss = new PedidoBE();

            // ObtenerPedidos codigo cliente 

            string getCodigoCliente = @"Select Codigo FROM Clientes WHERE IdUsuario=@IdUsuario";

            var codigo = ConnectionString.Query<int>(getCodigoCliente, param: new { IdUsuario = id });

            // obtener numeros pedidos por codigo cliente

            string getNumeroPedido = @"Select NumeroPedido,CodigoCliente,Fecha,Observacion FROM Pedidos WHERE CodigoCliente=@CodigoCliente";

            var pedido = ConnectionString.Query<PedidoBE>(getNumeroPedido, param: new { CodigoCliente = codigo });

              

            // obtener detalles por numero pedidos

            //string getDetallesPedidos = @"Select CodigoProducto,Cantidad,PrecioUnitario,NumeroPedido,NumeroItem FROM DetallesPedidos WHERE NumeroPedido=@NumeroPedido";

        

            //pedido.ListaCarro = ConnectionString.Query<DetallePedidoBE>(getDetallesPedidos, param: new { NumeroPedido = pedido.First().ListaCarro});


            Dictionary<int, PedidoBE> dic = new Dictionary<int, PedidoBE>();
            ConnectionString.Query<PedidoBE, DetallePedidoBE, int>
                (@"select p.*,d.* from Pedidos p Join DetallesPedidos d 
                            on p.SellerId = s.Id",
                (s, p) =>
                {
                    if (dic.ContainsKey(s.NumeroPedido))
                        dic[s.Id].Products.Add(p);
                    else
                    {
                        s.ListaCarro = new DetallePedidoBE();
                        s.ListaCarro.Add(p);
                        dic.Add(s.Id, s);
                    }
                    return s.Id;
                });
            var sellers = dic.Select(pair => pair.Value);
            return pedidoss;
        }
    }

}
