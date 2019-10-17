using CarroDeComprasCommon.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarroDeComprasBLL.Interfaces
{
    public interface IPedidoBLL
    {
        bool AgregarPedido(PedidoDTO pedidoDTO);

        IEnumerable<PedidoDTO> ObtenerPedidosXusuario(int idUsuario);

        IEnumerable<PedidoDTO> ObtenerPedidos();

        PedidoDTO ObtenerPedido(int numPedido);
    }
}
