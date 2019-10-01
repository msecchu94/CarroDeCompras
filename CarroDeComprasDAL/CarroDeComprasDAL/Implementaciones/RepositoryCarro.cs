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
        public SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionTable"].ConnectionString);




        public void AgragarCarro(int codigo, int cantidadProducto, int id)
        {
            try
            {
                string InsertCarro = @"INSERT INTO Carro(IdUsuario,Cantidad,CodigoProducto) VALUES(@id,@cantidadProducto,@codigo)";

                var result = ConnectionString.Execute(InsertCarro, param: new { id, cantidadProducto, codigo });
            }
            catch (Exception)
            {
                throw;
            }

        }

        public PedidoBE ObtenerCarro(int IdUsuario)
        {
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
            try
            {
                PedidoBE pedidoBE = new PedidoBE();
                pedidoBE.DetallesPedido = ConnectionString.Query<CarroBE, ProductoBE, DetallePedidoBE>(ObtenerCarro + " WHERE c.[IdUsuario]=@IdUsuario",(carro, producto) =>
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

        public void EliminarItem(int codigo, int IdUsuario)
        {
            string EliminarItem = @"DELETE FROM Carro WHERE CodigoProducto=@codigo AND IdUsuario=@IdUsuario ";
            try
            {
                var Eliminar = ConnectionString.Execute(EliminarItem, param: new { codigo, IdUsuario });
            }
            catch (Exception E)
            {
                System.Diagnostics.Debug.WriteLine(EliminarItem + "DELETE FROM Carro WHERE CodigoProducto=@codigo");
                throw E;
            }
        }

        public void VaciarCarro(int IdUsuario)
        {
            string VaciarCarro = @"DELETE FROM Carro WHERE  IdUsuario=@IdUsuario";

            try
            {
                ConnectionString.Execute(VaciarCarro, param: new { IdUsuario });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
