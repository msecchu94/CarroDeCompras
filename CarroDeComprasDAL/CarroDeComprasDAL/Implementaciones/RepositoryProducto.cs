using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using CarroDeComprasDAL.Interfaces;
using CarroDeComprasCommon.Entidad;
using CarroDeComprasCommon.DTO;
using CarroDeComprasDAL.Interfaces;
using CarroDeComprasBLL.Interfaces;

namespace CarroDeComprasDAL.Implementaciones
{
    public class RepositoryProducto : IRepositoryProducto
    {
        private readonly IConnectionFactory _connectionFactory;

        #region Querys

        private const string sqlObtener = @"SELECT 
                    p.[Codigo] 
                    ,p.[Nombre]
                    ,p.[Descripcion]
                    ,p.[IdMarca] 
                    ,p.[PrecioUnitario]
                    ,p.[Activo]
                    ,p.[UrlImange]

                    ,'split' as Split

                    ,m.[Id]
                    ,m.[Nombre]
                    FROM [Productos] p
                    INNER JOIN Marcas m ON p.IdMarca = m.Id";

        private const string sqlInsert = @"INSERT into Productos(Activo,Nombre,Descripcion,IdMarca,PrecioUnitario,UrlImange)
                                         VALUES(@Activo,@Nombre,@Descripcion,@IdMarca,@PrecioUnitario,@UrlImange)";

        private const string sqlObtenerId = @"SELECT p.[Codigo],p.[Nombre],p.[Descripcion],p.[IdMarca],p.[PrecioUnitario],p.[Activo],p.[UrlImange]
                                            FROM [Productos] p WHERE [Codigo]=@codigo";

        private const string sqlUpdate = @"UPDATE Productos SET Nombre=@Nombre,Descripcion=@Descripcion,IdMarca=@IdMarca,PrecioUnitario=@PrecioUnitario,
                                         Activo=@Activo,UrlImange=@UrlImange WHERE Codigo=@Codigo";


        #endregion

        public RepositoryProducto(IConnectionFactory ConnectionFactory)
        {
            this._connectionFactory = ConnectionFactory;
        }

        public IEnumerable<ProductoBE> ObtenerProductos()
        {
            using (var Connection = _connectionFactory.CreateConnection())
            {
                var lista = Connection.Query<ProductoBE, MarcaBE, ProductoBE>(sqlObtener,
                    (producto, marca) =>
                    {
                        producto.Marca = marca;
                        return producto;

                    }, splitOn: "Split");

                return lista;
            }
        }

        public bool AltaProducto(ProductoBE productoBE)
        {
            using (var Connection = _connectionFactory.CreateConnection())
            {

                var result = Connection.Execute(sqlInsert, param: productoBE);

                if (result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public ProductoBE ObtenerPorCodigo(int códigoProducto)
        {
            using (var Connection = _connectionFactory.CreateConnection())
            {
                ProductoBE productoBE = new ProductoBE();
                productoBE = Connection.Query<ProductoBE>(sqlObtenerId, param: new { Codigo = códigoProducto }).Single();

                return productoBE;
            }
        }

        public bool EditarProducto(ProductoBE productoBE)
        {
            using (var Connection = _connectionFactory.CreateConnection())
            {
                var result = Connection.Execute(sqlUpdate, param: productoBE);
                return result == 1;
            }
        }

        public IEnumerable<ProductoBE> ObtenerProductosActivos()
        {
            using (var Connection = _connectionFactory.CreateConnection())
            {
                var lista = Connection.Query<ProductoBE, MarcaBE, ProductoBE>(sqlObtener + " WHERE p.[Activo]=1", (producto, marca) =>
                   {
                       producto.Marca = marca;
                       return producto;

                   }, splitOn: "Split");

                return lista;
            }
        }
    }
}