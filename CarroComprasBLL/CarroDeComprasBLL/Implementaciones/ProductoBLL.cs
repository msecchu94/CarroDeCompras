using CarroDeComprasBLL.Interfaces;
using CarroDeComprasCommon.DTO;
using CarroDeComprasCommon.Entidad;
using CarroDeComprasDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarroDeComprasBLL.Implementaciones
{
    public class ProductoBLL : IProductoBLL
    {
        private IRepositoryProducto _repositoryProducto;

        public ProductoBLL(IRepositoryProducto repositoryProducto)
        {
            this._repositoryProducto = repositoryProducto;
        }

        public IEnumerable<ProductoDTO> ObtenerProductos()
        {
            try
            {
                var productosBE = _repositoryProducto.ObtenerProductos();
                var productosDTO = productosBE.Select(item => new ProductoDTO
                {
                    Activo = item.Activo,
                    Codigo = item.Codigo,
                    Descripcion = item.Descripcion,
                    IdMarca = item.IdMarca,
                    Nombre = item.Nombre,
                    PrecioUnitario = item.PrecioUnitario,
                    UrlImange = item.UrlImange,
                    Marca = new MarcaDTO
                    {
                        Id = item.Marca.Id,
                        Nombre = item.Marca.Nombre
                    }
                });

                return productosDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AltaProducto(ProductoDTO productosDTO)
        {
            ProductoBE productoBE = null;
            try
            {
                if (productosDTO != null)
                {
                    productoBE = new ProductoBE()
                    {
                        Nombre = productosDTO.Nombre,
                        Descripcion = productosDTO.Descripcion,
                        IdMarca = productosDTO.IdMarca,
                        PrecioUnitario = productosDTO.PrecioUnitario,
                        UrlImange = productosDTO.UrlImange,
                        Activo = productosDTO.Activo

                    };
                }
                var altaOK = _repositoryProducto.AltaProducto(productoBE);
                return altaOK;
            }
            catch (Exception ex)
            {

                throw ex;
            }



        }

        public ProductoDTO ObtenerPorId(int codigo)
        {
            ProductoDTO productosDTO = null;
            ProductoBE productosBE = null;


            try
            {
                productosBE = _repositoryProducto.ObtenerPorId(codigo);
                if (productosBE != null)
                {
                    productosDTO = new ProductoDTO()
                    {
                        Activo = productosBE.Activo,
                        Codigo = productosBE.Codigo,
                        Descripcion = productosBE.Descripcion,
                        IdMarca = productosBE.IdMarca,
                        Nombre = productosBE.Nombre,
                        PrecioUnitario = productosBE.PrecioUnitario,
                        UrlImange = productosBE.UrlImange
                        //Marca = new MarcaDTO
                        //{
                        //    Id = productosBE.Marca.Id,
                        //    Nombre = productosBE.Marca.Nombre
                        //}

                    };
                }
                return productosDTO;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool EditarProducto(ProductoDTO productoDTO)
        {
            ProductoBE productoBE = null;
            try
            {
                if (productoDTO != null)
                {
                    productoBE = new ProductoBE()
                    {
                        Codigo = productoDTO.Codigo,
                        Activo = productoDTO.Activo,
                        Nombre = productoDTO.Nombre,
                        Descripcion = productoDTO.Descripcion,
                        IdMarca = productoDTO.IdMarca,
                        PrecioUnitario = productoDTO.PrecioUnitario,
                        UrlImange = productoDTO.UrlImange

                    };
                }
                var altaOK = _repositoryProducto.EditarProducto(productoBE);
                return altaOK;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}

