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

namespace CarroDeComprasDAL.Implementaciones
{
    public class RepositoryProducto : IRepositoryProducto
    {
        public SqlConnection ConnectionString = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionTable"].ConnectionString);

        private string sqlObtener = @"SELECT 
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

        private string sqlObtenerId = @"SELECT 
                    p.[Codigo] 
                    ,p.[Nombre]
                    ,p.[Descripcion]
                    ,p.[IdMarca] 
                    ,p.[PrecioUnitario]
                    ,p.[Activo]
                    ,p.[UrlImange]
                    FROM [Productos] p
                    WHERE [Codigo]=@codigo";


        public IEnumerable<ProductoBE> ObtenerProductos()
        {
            try
            {
                var lista = ConnectionString.Query<ProductoBE, MarcaBE, ProductoBE>(sqlObtener, (producto, marca) =>
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

        public bool AltaProducto(ProductoBE productoBE)
        {
            try
            {
                string sql = @"INSERT into Productos(Activo,Nombre,Descripcion,IdMarca,PrecioUnitario,UrlImange)
                 VALUES(@Activo,@Nombre,@Descripcion,@IdMarca,@PrecioUnitario,@UrlImange)";

                var result = ConnectionString.Execute(sql, param: productoBE);

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

        public ProductoBE ObtenerPorId(int codigo)
        {
            try
            {
                ProductoBE productoBE = new ProductoBE();

                productoBE = ConnectionString.Query<ProductoBE>(sqlObtenerId, param: new { Codigo = codigo }).Single();

                return productoBE;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool EditarProducto(ProductoBE productoBE)
        {
            try
            {
                string sql = @"UPDATE Productos 
                                SET Nombre=@Nombre,
                                Descripcion=@Descripcion,
                                IdMarca=@IdMarca,
                                PrecioUnitario=@PrecioUnitario,
                                Activo=@Activo,
                                UrlImange=@UrlImange
                                WHERE Codigo=@Codigo";


                var result = ConnectionString.Execute(sql, param: productoBE);
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
}