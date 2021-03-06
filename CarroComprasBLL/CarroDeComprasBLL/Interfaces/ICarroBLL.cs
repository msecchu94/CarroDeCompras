﻿using CarroDeComprasCommon.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarroDeComprasBLL.Interfaces
{
    public interface ICarroBLL
    {
        void AgregarCarro(int _codigoProducto, int cantidadProducto, int idUsuario);
        PedidoDTO ObtenerCarro(int IdUsuario);
        void EliminarItem(int codigoProducto, int IdUsuario);
        void VaciarCarro(int IdUsuario);
        void ModificarCarro(int _codigoProducto, int suma);
        bool CompararContenido(int _codigoProducto,int idUsuario);
    }
}
