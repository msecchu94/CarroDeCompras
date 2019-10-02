using CarroDeComprasBLL.Interfaces;
using CarroDeComprasCommon.DTO;
using CarroDeComprasCommon.Entidad;
using CarroDeComprasDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace CarroDeComprasBLL.Implementaciones
{
    public class ProductoBLL : IProductoBLL
    {
        private IRepositoryProducto _repositoryProducto;

        public ProductoBLL(IRepositoryProducto RepositoryProducto)
        {
            this._repositoryProducto = RepositoryProducto;
        }

        public IEnumerable<ProductoDTO> ObtenerProductos()
        {
            try
            {
                var productosBE = _repositoryProducto.ObtenerProductos();
                var productosDTO = Mapper.Map<IEnumerable<ProductoDTO>>(productosBE);

                return productosDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AltaProducto(ProductoDTO productosDTO)
        {
            try
            {
                var productoBE = Mapper.Map<ProductoBE>(productosDTO);
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

            try
            {
                var productosBE = _repositoryProducto.ObtenerPorId(codigo);

                var productosDTO = Mapper.Map<ProductoDTO>(productosBE);

                return productosDTO;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EditarProducto(ProductoDTO productoDTO)
        {
            try
            {
                var productoBE = Mapper.Map<ProductoBE>(productoDTO);

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

