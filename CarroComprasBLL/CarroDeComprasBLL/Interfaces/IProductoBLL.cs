using CarroDeComprasCommon.DTO;
using CarroDeComprasCommon.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarroDeComprasBLL.Interfaces
{
    public interface IProductoBLL
    {
        IEnumerable<ProductoDTO> ObtenerProductos();

        IEnumerable<ProductoDTO> ObtenerProductosActivos();

        bool AltaProducto(ProductoDTO productosDTO);

        ProductoDTO ObtenerPorCodigo(int códigoProducto);

        bool EditarProducto(ProductoDTO productoDTO);
    }
}
