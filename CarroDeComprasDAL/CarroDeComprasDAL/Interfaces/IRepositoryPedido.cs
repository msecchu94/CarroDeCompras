﻿using CarroDeComprasCommon.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarroDeComprasDAL.Interfaces
{
    public interface IRepositoryPedido
    {
        bool AgregarPedido(PedidoBE pedidoBE);
        IEnumerable<PedidoBE> ObtenerPedidosXusuario(int idUsuario);
        IEnumerable<PedidoBE> ObtenerPedidos();
        PedidoBE ObtenerPedido(int numPedido);
    }
}
