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

        bool AltaProducto(ProductoDTO productosDTO);

        ProductoDTO ObtenerPorId(int codigo);

        bool EditarProducto(ProductoDTO productoDTO);

    }
}
