using CarroDeComprasCommon.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarroDeComprasDAL.Interfaces
{
    public interface IRepositoryCarro
    {
        void AgragarCarro(int codigo, int cantidadProducto,int id);
        PedidoBE ObtenerCarro(int IdUsuario);
        void EliminarItem(int codigo, int IdUsuario);
        void VaciarCarro(int IdUsuario);
    }
}
