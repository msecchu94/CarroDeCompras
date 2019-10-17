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

        #region QueryObtener

        string sqlObtener = @"SELECT 
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
        #endregion

        public RepositoryProducto(IConnectionFactory ConnectionFactory)
        {
            this._connectionFactory = ConnectionFactory;
        }

        public IEnumerable<ProductoBE> ObtenerProductos()
        {


            using (var Connection = _connectionFactory.CreateConnection())
            {
                try
                {
                    var lista = Connection.Query<ProductoBE, MarcaBE, ProductoBE>(sqlObtener, (producto, marca) =>
                                         {
                                             producto.Marca = marca;
                                             return producto;

                                         }, splitOn: "Split");

                    return lista;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        public bool AltaProducto(ProductoBE productoBE)
        {
            #region Query

            string sqlInsert = @"INSERT into Productos(Activo,Nombre,Descripcion,IdMarca,PrecioUnitario,UrlImange)
                 VALUES(@Activo,@Nombre,@Descripcion,@IdMarca,@PrecioUnitario,@UrlImange)";

            #endregion

            using (var Connection = _connectionFactory.CreateConnection())
            {
                try
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
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public ProductoBE ObtenerPorCodigo(int códigoProducto)
        {
            #region Query

            string sqlObtenerId = @"SELECT 
                    p.[Codigo] 
                    ,p.[Nombre]
                    ,p.[Descripcion]
                    ,p.[IdMarca] 
                    ,p.[PrecioUnitario]
                    ,p.[Activo]
                    ,p.[UrlImange]
                    FROM [Productos] p
                    WHERE [Codigo]=@codigo";
            #endregion

            using (var Connection = _connectionFactory.CreateConnection())
            {
                try
                {
                    ProductoBE productoBE = new ProductoBE();
                    productoBE = Connection.Query<ProductoBE>(sqlObtenerId, param: new { Codigo = códigoProducto }).Single();

                    return productoBE;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool EditarProducto(ProductoBE productoBE)
        {
            #region QUERY


            string sql = @"UPDATE Productos 
                                SET Nombre=@Nombre,
                                Descripcion=@Descripcion,
                                IdMarca=@IdMarca,
                                PrecioUnitario=@PrecioUnitario,
                                Activo=@Activo,
                                UrlImange=@UrlImange
                                WHERE Codigo=@Codigo";

            #endregion


            using (var Connection = _connectionFactory.CreateConnection())
            {
                try
                {
                    var result = Connection.Execute(sql, param: productoBE);
                    if (result == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        public IEnumerable<ProductoBE> ObtenerProductosActivos()
        {
            using (var Connection = _connectionFactory.CreateConnection())
            {
                try
                {
                    var lista = Connection.Query<ProductoBE, MarcaBE, ProductoBE>(sqlObtener + " WHERE p.[Activo]=1", (producto, marca) =>
                     {
                         producto.Marca = marca;
                         return producto;

                     }, splitOn: "Split");

                    return lista;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}