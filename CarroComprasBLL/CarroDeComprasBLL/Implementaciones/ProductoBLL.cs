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
            var productosBE = _repositoryProducto.ObtenerProductos();
            var productosDTO = Mapper.Map<IEnumerable<ProductoDTO>>(productosBE);

            return productosDTO;
        }

        public bool AltaProducto(ProductoDTO productosDTO)
        {
            var productoBE = Mapper.Map<ProductoBE>(productosDTO);
            var altaOK = _repositoryProducto.AltaProducto(productoBE);

            return altaOK;
        }

        public ProductoDTO ObtenerPorCodigo(int códigoProducto)
        {
            var productosBE = _repositoryProducto.ObtenerPorCodigo(códigoProducto);
            var productosDTO = Mapper.Map<ProductoDTO>(productosBE);

            return productosDTO;
        }

        public bool EditarProducto(ProductoDTO productoDTO)
        {
            var productoBE = Mapper.Map<ProductoBE>(productoDTO);
            var altaOK = _repositoryProducto.EditarProducto(productoBE);

            return altaOK;
        }

        public IEnumerable<ProductoDTO> ObtenerProductosActivos()
        {
            var productosBE = _repositoryProducto.ObtenerProductosActivos();
            var productosDTO = Mapper.Map<IEnumerable<ProductoDTO>>(productosBE);

            return productosDTO;
        }
    }
}

