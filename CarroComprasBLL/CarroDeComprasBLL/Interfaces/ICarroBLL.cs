using CarroDeComprasCommon.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarroDeComprasBLL.Interfaces
{
    public interface ICarroBLL
    {
        void AgregarCarro(int codigo, int cantidadProducto,int id);
        PedidoDTO ObtenerCarro(int IdUsuario);
        void EliminarItem(int codigo, int IdUsuario);
        void VaciarCarro(int IdUsuario);

    }
}
